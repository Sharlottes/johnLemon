using Assets.Scripts.Utils.Keybind;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public float turnSpeed = 20f;
        public float moveSpeed = 1f;

        Animator m_Animator;
        Rigidbody m_Rigidbody;
        Vector3 m_Movement;
        Quaternion m_Rotation = Quaternion.identity;
        AudioSource m_AudioSource;

        int[] keybindIds = new int[4];

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_AudioSource = GetComponent<AudioSource>();
            KeyBindManager.Instance
                .Bind<OrBind>(KeyCodeUtils.Horizontal)
                    .Or<OrBind>(KeyCodeUtils.Vertical)
                    .Then((KeyCode[] codes, BindObject bind) =>
                    {
                        Vector2 xz = new();

                        foreach (KeyCode code in codes)
                        {
                            Vector2 res = code switch
                            {
                                KeyCode.A or KeyCode.LeftArrow => new(-1, 0),
                                KeyCode.D or KeyCode.RightArrow => new(1, 0),
                                KeyCode.W or KeyCode.UpArrow => new(0, 1),
                                KeyCode.S or KeyCode.DownArrow => new(0, -1),
                                _ => throw new NotImplementedException(),
                            };
                            if (xz.x == 0) xz.x = res.x;
                            if (xz.y == 0) xz.y = res.y;
                        }
                        m_Movement = new(xz[0], 0f, xz[1]);
                        m_Movement.Normalize();

                        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
                        m_Rotation = Quaternion.LookRotation(desiredForward);

                        m_Animator.SetBool("IsWalking", true);
                        if (!m_AudioSource.isPlaying) m_AudioSource.Play();
                    })
                    .Else(() =>
                    {
                        m_Animator.SetBool("IsWalking", false);
                        m_AudioSource.Stop();
                    })
                    .GetID(out keybindIds[0])
                .Bind(KeyCode.E)
                    .Then(() =>
                    {
                        //TODO: 아이템 사용
                    })
                    .GetID(out keybindIds[1])
                .Bind(KeyCode.LeftShift, KeyCode.RightShift)
                    .Then(() =>
                    {
                        //TODO: Run();
                    })
                    .GetID(out keybindIds[2])
                .Bind(KeyCode.LeftControl, KeyCode.RightControl)
                    .Then(() =>
                    {
                        //TODO: Slow();
                    })
                    .GetID(out keybindIds[3]);
        }

        private void OnDestroy()
        {
            foreach(int id in keybindIds)
            {

                KeyBindManager.Instance.UnBind(id);
            }
        }


        void OnAnimatorMove()
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }
}
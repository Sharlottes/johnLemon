using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Structs;

namespace Assets.Scripts
{
    public class WaypointPatrol : MonoBehaviour
    {
        public float turnSpeed = 10f;
        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;
        public float detectRange = 2f;
        public GameObject detectIndicatorPref;

        bool m_IsFound;
        int m_CurrentWaypointIndex;
        PatrolDetectIndicator m_detectIndicator;
        SingleCoroutineController m_LookAroundCoroutineController;

        Animator m_Animator;

        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_LookAroundCoroutineController = new(this, LookAroundAnimation);

            GameObject DetectIndicatorGO = Instantiate(detectIndicatorPref, Canvas.Instance.transform);
            DetectIndicatorGO.transform.SetSiblingIndex(0);
            m_detectIndicator = DetectIndicatorGO.GetComponent<PatrolDetectIndicator>();
            navMeshAgent.SetDestination(waypoints[0].position);
        }

        IEnumerator LookAroundAnimation()
        {
            bool toLeft = false;
            float t = 0;
            while(true)
            {
                if (t >= 1) toLeft = true;
                float rotateAmount = (toLeft ? -1 : 1);
                t += Time.deltaTime * rotateAmount;
                transform.Rotate(new(0, 1), Convert.ToSingle(rotateAmount * Math.PI / 4f));
                yield return null;
            }
        }

        float m_delay;
        void Update()
        {
            m_detectIndicator.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f));

            Vector3 targetDirection = Player.Instance.transform.position - transform.position;
            if (m_detectIndicator.IsFound)
            {
                //TODO: find state -> enum 리팩토링 시급 코드 겁나 더러움
                if (!m_IsFound)
                {
                    StarGroup.Instance.StarPoint++;
                    m_IsFound = true;
                }

                if (m_Animator.parameters.Length > 0 && m_Animator.parameters[0].name == "IsFound")
                    m_Animator.SetBool("IsFound", true);
                
                navMeshAgent.isStopped = false;
                m_LookAroundCoroutineController.Stop();
                navMeshAgent.SetDestination(Player.Instance.transform.position + Vector3.up);
            }
            else if (
                Physics.Raycast(
                    transform.position, targetDirection + Vector3.up,
                    out RaycastHit raycastHit,
                    detectRange
                ) &&
                raycastHit.collider.transform == Player.Instance.transform
            )
            {
                m_detectIndicator.isDetecting = true;
                m_LookAroundCoroutineController.Start();
                m_delay = 0.1f;
            }
            else
            {
                navMeshAgent.isStopped = m_detectIndicator.warnProgress > 0.5f;

                if (m_delay > 0)
                {
                    m_delay -= Time.deltaTime;
                    return;
                }
                m_detectIndicator.isDetecting = false;
                m_LookAroundCoroutineController.Stop();

                if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
                {
                    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
            }
        }
    }
}
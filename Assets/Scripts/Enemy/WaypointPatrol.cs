using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Utils.Singletons;
using Assets.Scripts.UI.Scenes.GameScene;
using Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public class WaypointPatrol : MonoBehaviour
    {
        public float turnSpeed = 10f;
        public Transform[] waypoints;
        public float detectRange = 2f;
        public GameObject detectIndicatorPref;

        bool m_IsFound, m_IsFinding;
        int m_CurrentWaypointIndex;

        Animator m_Animator;
        NavMeshAgent m_NavMeshAgent;
        PatrolDetectIndicator m_detectIndicator;
        SingleCoroutineController m_LookAroundCoroutineController;

        void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
            m_LookAroundCoroutineController = new(LookAroundAnimation);
            
            GameObject DetectIndicatorGO = Instantiate(detectIndicatorPref, Canvas.Instance.transform);
            DetectIndicatorGO.transform.SetSiblingIndex(0);
            m_detectIndicator = DetectIndicatorGO.GetComponent<PatrolDetectIndicator>();
            m_NavMeshAgent.SetDestination(waypoints[0].position);
        }

        IEnumerator LookAroundAnimation()
        {
            bool toLeft = false;
            float t = 0;
            const float rotateSpeed = 10;

            while (true)
            {
                t += (toLeft ? -1 : 1) * Time.deltaTime * rotateSpeed;
                if (t > 5) toLeft = !toLeft;
                transform.Rotate(new(0, 1, 0), 30f * (toLeft ? -1 : 1) * Time.deltaTime * rotateSpeed);
                yield return null;
            }
        }

        void Update()
        {
            m_detectIndicator.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f));

            m_NavMeshAgent.isStopped = m_IsFinding;

            if (!m_IsFound || !m_detectIndicator.isDetecting || !m_IsFinding) PatrolUpdate();

            if (m_Animator.parameters.Length > 0 && m_Animator.parameters[0].name == "IsFound")
                m_Animator.SetBool("IsFound", m_IsFound);

            Vector3 targetDirection = Player.Instance.transform.position - transform.position;
            if (m_detectIndicator.indicateLevel == FindLevel.Find)
            {
                if (!m_IsFound)
                {
                    StarGroup.Instance.StarPoint++;
                    m_IsFound = true;
                }

                m_NavMeshAgent.SetDestination(Player.Instance.transform.position + Vector3.up);
            }
            else if (
                Physics.Raycast(
                    transform.position, targetDirection + Vector3.up,
                    out RaycastHit raycastHit,
                    detectRange
                ) && raycastHit.collider.transform == Player.Instance.transform
            )
            {
                m_IsFinding = true;
                m_detectIndicator.isDetecting = true;
                m_LookAroundCoroutineController.Start();

                Timer.Instance.SetTimeout(() =>
                {
                    if (m_IsFinding) return;
                    m_detectIndicator.isDetecting = false;
                    m_LookAroundCoroutineController.Stop();
                }, 1f);
            }
            else m_IsFinding = false;

        }

        void PatrolUpdate()
        {
            if (m_NavMeshAgent.remainingDistance < m_NavMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                m_NavMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }
    }
}
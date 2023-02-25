using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

namespace Assets.Scripts
{
    public class WaypointPatrol : MonoBehaviour
    {
        public float turnSpeed = 10f;
        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;
        public float detectRange = 2f;
        public GameObject detectIndicatorPref;

        int m_CurrentWaypointIndex;
        PatrolDetectIndicator m_detectIndicator;
        Coroutine m_AnimationCoroutine;

        void Start()
        {
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
                transform.Rotate(new(0, 1), rotateAmount / 6);
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
                navMeshAgent.isStopped = false;
                if (m_AnimationCoroutine != null)
                {
                    StopCoroutine(m_AnimationCoroutine);
                    m_AnimationCoroutine = null;
                }
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
                float distance = Vector3.Distance(Player.Instance.transform.position, transform.position);
                Debug.Log(distance);

                    m_detectIndicator.isDetecting = true;
                    m_delay = 0.1f;
                    m_AnimationCoroutine ??= StartCoroutine(LookAroundAnimation());
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

                if (m_AnimationCoroutine != null)
                {
                    StopCoroutine(m_AnimationCoroutine);
                    m_AnimationCoroutine = null;
                }
                if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
                {
                    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Observer : MonoBehaviour
    {
        bool m_IsPlayerInRange;

        void OnTriggerEnter(Collider other)
        {
            if (other.transform == Player.Instance.transform)
                m_IsPlayerInRange = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.transform == Player.Instance.transform)
                m_IsPlayerInRange = false;
        }

        void Update()
        {
            if (m_IsPlayerInRange)
            {
                Vector3 direction = Player.Instance.transform.position - transform.position + Vector3.up;
                Ray ray = new(transform.position, direction);
                if (Physics.Raycast(ray, out RaycastHit raycastHit) && raycastHit.collider.transform == Player.Instance.transform)
                {
                    GameEnding.Instance.CaughtPlayer();
                }
            }
        }
    }
}
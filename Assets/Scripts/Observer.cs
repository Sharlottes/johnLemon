using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Observer : MonoBehaviour
    {
        public Transform player;
        public GameEnding gameEnding;

        bool m_IsPlayerInRange;

        void OnTriggerEnter(Collider other)
        {
            if (other.transform == player)
                m_IsPlayerInRange = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.transform == player)
                m_IsPlayerInRange = false;
        }

        void Update()
        {
            if (m_IsPlayerInRange)
            {
                Vector3 direction = player.position - transform.position + Vector3.up;
                Ray ray = new(transform.position, direction);
                if (Physics.Raycast(ray, out RaycastHit raycastHit) && raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerName : MonoBehaviour
    {
        public string playerName;
        public GameObject nameTextPref;

        TMP_Text m_nameText;

        void Start()
        {
            GameObject nameTextGO = Instantiate(nameTextPref, Canvas.Instance.transform);
            nameTextGO.transform.SetSiblingIndex(0);
            m_nameText = nameTextGO.GetComponent<TMP_Text>();
            m_nameText.SetText(playerName);
            m_nameText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f));
        }

        private void Update()
        {
            m_nameText.transform.position = Vector3.Lerp(
                m_nameText.transform.position, 
                Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f)), 
                5 * Time.deltaTime
            );
        }
    }
}
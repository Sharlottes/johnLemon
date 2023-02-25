using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public Canvas canvas;
    public string playerName;
    public GameObject nameTextPref;
    TMP_Text m_nameText;

    void Start()
    {
        m_nameText = Instantiate(nameTextPref, canvas.transform).GetComponent<TMP_Text>();
        m_nameText.SetText(playerName);
        m_nameText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(1, 1.35f));
    }
}

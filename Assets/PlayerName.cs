using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public Canvas canvas;
    public string playerName;
    public GameObject nameTextPref;
    public float nameTextFollowSpeed = 5;

    TMP_Text m_nameText;

    void Start()
    {
        m_nameText = Instantiate(nameTextPref, canvas.transform).GetComponent<TMP_Text>();
        m_nameText.SetText(playerName);
        m_nameText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f));
    }

    private void Update()
    {
        m_nameText.transform.position = Vector3.Lerp(m_nameText.transform.position, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f)), nameTextFollowSpeed * Time.deltaTime);
    }
}

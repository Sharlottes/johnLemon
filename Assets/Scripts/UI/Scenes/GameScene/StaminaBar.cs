using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public GameObject staminaBarPref;
    public float maxStamina, currentStamina;
    public float Progress => currentStamina / maxStamina;
    public Color from = Color.white, to = Color.red;

    float width;
    Image m_Image;
    RectTransform m_BackgroundRect, m_Rect;

    void Start()
    {
        currentStamina = maxStamina;
        GameObject staminaBarGO = Instantiate(staminaBarPref, Canvas.Instance.transform);
        staminaBarGO.transform.SetSiblingIndex(0);

        m_Image = staminaBarGO.transform.GetChild(0).GetComponent<Image>();
        m_BackgroundRect = staminaBarGO.GetComponent<RectTransform>();
        m_Rect = staminaBarGO.transform.GetChild(0).GetComponent<RectTransform>();
        width = m_Rect.sizeDelta.x;

        m_BackgroundRect.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -0.25f));
    }

    void Update()
    {
        m_BackgroundRect.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -0.25f));
        m_Rect.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -0.25f)) - new Vector3(width * (1 - Progress) / 2, 0f);

        m_Image.color = Color.Lerp(from, to, Progress);
        m_Rect.sizeDelta = new(width * Progress, m_Rect.sizeDelta.y);
    }
}

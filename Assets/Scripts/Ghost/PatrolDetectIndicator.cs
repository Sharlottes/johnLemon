using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public enum FindLevel
    {
        Idle,
        Warn,
        Find
    }

    public class PatrolDetectIndicator : MonoBehaviour
    {
        [SerializeField] Sprite defaultIcon, warnIcon, findIcon;
        [SerializeField] Image warnBar, findBar, icon;
        [SerializeField] float m_DetectSpeed = 2;
        [SerializeField] float m_Progress;

        public bool isDetecting = false;
        public FindLevel indicateLevel = FindLevel.Idle;

        void Update()
        {
            float speedDelta = Time.deltaTime * m_DetectSpeed;
            m_Progress = Math.Clamp(m_Progress + speedDelta * (isDetecting ? 1 : -1), 0, 2);

            indicateLevel = m_Progress >= 2 ? FindLevel.Find : m_Progress >= 0.5f ? FindLevel.Warn : FindLevel.Idle;

            icon.sprite = indicateLevel switch
            {
                FindLevel.Idle => defaultIcon,
                FindLevel.Warn => warnIcon,
                FindLevel.Find => findIcon,
                _ => throw new Exception("wrong enum value!"),
            };
            warnBar.fillAmount = m_Progress;
            findBar.fillAmount = m_Progress - 1;
        }
    }
}
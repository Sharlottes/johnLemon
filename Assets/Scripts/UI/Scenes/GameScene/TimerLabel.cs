using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.GameScene
{
    public class TimerLabel : MonoBehaviour
    {
        TMP_Text timer;

        void Start()
        {
            timer = GetComponent<TMP_Text>();
        }

        void Update()
        {
            timer.text = TimeSpan.FromSeconds(Time.time).ToString("mm\\:ss\\.fff");
            PlayerPrefs.SetFloat("playtime", Time.time);
        }
    }
}
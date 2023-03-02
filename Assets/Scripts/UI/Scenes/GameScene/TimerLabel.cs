using System;
using System.Collections;
using System.Collections.Generic;
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

        float t = 0;
        void Update()
        {
            t += Time.deltaTime;
            timer.text = TimeSpan.FromSeconds(t).ToString("mm\\:ss\\.fff");
            PlayerPrefs.SetFloat("playtime", t);
        }
    }
}
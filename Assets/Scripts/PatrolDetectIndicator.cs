using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatrolDetectIndicator : MonoBehaviour
{
    public Sprite defaultIcon, warnIcon, findIcon;
    public Image warnBar, findBar, icon;
    public bool isDetecting = false;
    public bool IsFound => findProgress >= 1 && icon.sprite == findIcon;
    public float detectSpeed = 2f;

    public float warnProgress, findProgress;

    void Update()
    {
        if(isDetecting)
        {
            if (warnProgress < 1)
            {
                warnProgress += Time.deltaTime * detectSpeed;
            }
            else if (findProgress < 1)
            {
                findProgress += Time.deltaTime * detectSpeed;
            }
        }
        else
        {
            if (findProgress > 0)
            {
                findProgress -= Time.deltaTime * detectSpeed;
            }
            else if (warnProgress > 0)
            {
                warnProgress -= Time.deltaTime * detectSpeed;
            }
        }

        if (findProgress >= 1) icon.sprite = findIcon;
        else if (warnProgress > 0) icon.sprite = warnIcon;
        else icon.sprite = defaultIcon;

        warnBar.fillAmount = warnProgress;
        findBar.fillAmount = findProgress;
    }
}

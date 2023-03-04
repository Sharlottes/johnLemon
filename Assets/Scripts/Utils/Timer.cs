using System;
using UnityEngine;
using Assets.Scripts.Utils.Singletons;
using System.Collections;

namespace Assets.Scripts.Utils
{
    internal class Timer : LazyDDOLSingletonMonoBehaviour<Timer>
    {
        IEnumerator SetTimeoutCoroutine(Action callback, float durationInSecond)
        {
            yield return new WaitForSeconds(durationInSecond);
            callback();
        }
        public void SetTimeout(Action callback, float durationInSecond)
        {
            StartCoroutine(SetTimeoutCoroutine(callback, durationInSecond));
        }
    }
}

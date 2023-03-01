using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Transitions
{
    public abstract class ScreenTransition : MonoBehaviour
    {
        public abstract IEnumerator Run(float fadeDuration);
    }
}

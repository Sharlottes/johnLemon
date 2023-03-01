using Assets.Scripts.Structs.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Transitions
{
    public class ScreenFadeInTransition : ScreenTransition
    {
        Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public override IEnumerator Run(float fadeDuration)
        {
            float t = 0;
            while (true)
            {
                t += Time.deltaTime;
                if (t > fadeDuration) break;
                image.color = new(0, 0, 0, t / fadeDuration);
                yield return null;
            }
        }
    }
}

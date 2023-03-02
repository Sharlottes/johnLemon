using Assets.Scripts.Utils.Transitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.GameOverScene
{
    public class ExitButton : MonoBehaviour
    {
        public void OnClick() => StartCoroutine(GameExit());

        IEnumerator GameExit()
        {
            yield return ScreenTransitionController.Instance.StartTransition<ScreenFadeInTransition>(1);
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
using Assets.Scripts.Utils.Transitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.GameOverScene
{
    public class RestartButton : MonoBehaviour
    {
        public void OnClick() => ScreenTransitionController.Instance.ChangeScene
            <ScreenFadeInTransition, ScreenFadeOutTransition>
            ("GameScene", 0.5f, 1);
    }
}
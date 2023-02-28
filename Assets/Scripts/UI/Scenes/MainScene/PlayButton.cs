using Assets.Scripts.Transitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.MainScene
{
    public class PlayButton : MonoBehaviour
    {
        public void OnClick() => ScreenTransitionController.Instance.ChangeScene<ScreenFadeInTransition, ScreenFadeOutTransition>("GameScene", 0.5f, 1);

    }
}
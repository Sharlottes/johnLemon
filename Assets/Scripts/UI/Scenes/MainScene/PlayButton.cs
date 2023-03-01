using Assets.Scripts.Transitions;
using Assets.Scripts.Utils.Keybind;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.MainScene
{
    public class PlayButton : MonoBehaviour
    {
        private void Start()
        {
            KeyBindManager.Instance
                .Bind(new() { once = true })
                    .Is<OrBind>(KeyCode.Return, KeyCode.Space)
                    .Then(OnClick);
        }
        public void OnClick() => ScreenTransitionController.Instance.ChangeScene<ScreenFadeInTransition, ScreenFadeOutTransition>("GameScene", 0.5f, 1);

    }
}
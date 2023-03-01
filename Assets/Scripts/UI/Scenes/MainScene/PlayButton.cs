using Assets.Scripts.Transitions;
using Assets.Scripts.Utils.Keybind;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.MainScene
{
    public class PlayButton : MonoBehaviour
    {
        int id;

        private void Start()
        {
            KeyBindManager.Instance
                .Bind<OrBind>(new BindOptions(){ once = true }, KeyCode.Return, KeyCode.Space)
                    .Then(OnClick)
                    .GetID(out id);
        }

        private void OnDestroy() {
            KeyBindManager.Instance.UnBind(id);
        }

        public void OnClick() => ScreenTransitionController.Instance.ChangeScene
            <ScreenFadeInTransition, ScreenFadeOutTransition>
            ("GameScene", 0.5f, 1);
    }
}
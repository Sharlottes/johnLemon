using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.GameOverScene
{
    public class ExitButton : MonoBehaviour
    {
        public void OnClick() => Application.Quit();
    }
}
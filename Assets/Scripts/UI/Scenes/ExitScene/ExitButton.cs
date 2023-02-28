using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.ExitScene
{
    public class ExitButton : MonoBehaviour
    {
        public void OnClick() => Application.Quit();
    }
}
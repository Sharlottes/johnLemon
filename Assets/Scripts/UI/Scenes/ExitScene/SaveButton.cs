using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.ExitScene
{
    public class SaveButton : MonoBehaviour
    {
        public void OnClick() => PlayerPrefs.Save();
    }
}
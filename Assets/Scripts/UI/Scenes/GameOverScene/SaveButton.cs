using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Scenes.GameOverScene
{
    public class SaveButton : MonoBehaviour
    {
        public void OnClick() => PlayerPrefs.Save();
    }
}
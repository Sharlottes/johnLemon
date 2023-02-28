using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Scenes.ExitScene
{
    public class RestartButton : MonoBehaviour
    {
        public void OnClick() => SceneManager.LoadScene("GameScene");
    }
}
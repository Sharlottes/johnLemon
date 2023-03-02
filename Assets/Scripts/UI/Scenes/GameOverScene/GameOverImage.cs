using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scenes.GameOverScene
{
    public class GameOverImage : MonoBehaviour
    {
        public Sprite exitImage, caughtImage;

        private void Start()
        {
            GetComponent<Image>().sprite = GameOverController.Instance.isWin ? exitImage : caughtImage;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Structs.Singleton;
using Assets.Scripts.Structs;
using Unity.VisualScripting;

namespace Assets.Scripts
{
    public class GameEnding : SingletonMonoBehaviour<GameEnding>
    {
        public float fadeDuration = 1f;
        public float displayImageDuration = 1f;
        readonly SingleCoroutineController endGameCoroutineController = new();

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Player.Instance.gameObject)
            {
                WinPlayer();
            }
        }

        public void WinPlayer()
        {
            endGameCoroutineController.Start(GameOverController.Instance.EndGame(true));
        }
        public void CaughtPlayer()
        {
            endGameCoroutineController.Start(GameOverController.Instance.EndGame(false));
        }
    }
}

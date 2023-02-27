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
        public CanvasGroup exitBackgroundImageCanvasGroup, caughtBackgroundImageCanvasGroup;
        public AudioSource exitAudio, caughtAudio;

        SingleCoroutineController m_EndLevelCoroutineController;

        private void Start()
        {
            m_EndLevelCoroutineController = new(this);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Player.Instance.gameObject)
            {
                WinPlayer();
            }
        }

        //TODO: 아니 솔직히 이거 씬 전환하는거 같은데
        public void WinPlayer()
        {
            m_EndLevelCoroutineController.Start(EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio));
        }
        public void CaughtPlayer()
        {
            m_EndLevelCoroutineController.Start(EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio));
        }

        IEnumerator EndLevel(CanvasGroup imageCanvasGroup, bool doAutoRestart, AudioSource audio)
        {
            audio.Play();
            float t = 0;
            while(true)
            {
                t += Time.deltaTime;
                imageCanvasGroup.alpha = t / fadeDuration;

                if (t > fadeDuration + displayImageDuration)
                {
                    if (doAutoRestart) RestartGame();
                    yield break;
                }
            }
        }

        public void RestartGame() => SceneManager.LoadScene(0);
        public void EndGame() => Application.Quit();
        public void SaveData() => PlayerPrefs.Save();
    }
}

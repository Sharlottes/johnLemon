using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Structs.Singleton;

namespace Assets.Scripts
{
    public class GameEnding : SingletonMonoBehaviour<GameEnding>
    {
        public float fadeDuration = 1f;
        public float displayImageDuration = 1f;
        public CanvasGroup exitBackgroundImageCanvasGroup, caughtBackgroundImageCanvasGroup;
        public AudioSource exitAudio, caughtAudio;

        bool m_IsPlayerAtExit, m_IsPlayerCaught;
        float m_Timer;
        bool m_HasAudioPlayed;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Player.Instance.gameObject)
            {
                m_IsPlayerAtExit = true;
            }
        }

        public void CaughtPlayer()
        {
            m_IsPlayerCaught = true;
        }

        void Update()
        {
            if (m_IsPlayerAtExit)
            {
                EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
            }
            else if (m_IsPlayerCaught)
            {
                EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
            }
        }

        void EndLevel(CanvasGroup imageCanvasGroup, bool doAutoRestart, AudioSource audioSource)
        {
            if (!m_HasAudioPlayed)
            {
                audioSource.Play();
                m_HasAudioPlayed = true;
            }
            m_Timer += Time.deltaTime;
            imageCanvasGroup.alpha = m_Timer / fadeDuration;

            if (m_Timer > fadeDuration + displayImageDuration)
            {
                if (doAutoRestart) RestartGame();
            }
        }

        public void RestartGame() => SceneManager.LoadScene(0);
        public void EndGame() => Application.Quit();
    }
}
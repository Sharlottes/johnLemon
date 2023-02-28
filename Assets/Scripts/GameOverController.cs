using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Structs.Singleton;
using UnityEngine.SceneManagement;
using Assets.Scripts.Transitions;
using Assets.Scripts.Utils;
using UnityEngine.Analytics;

namespace Assets.Scripts
{
    public class GameOverController : DDOLSingletonMonoBehaviour<GameOverController>
    {
        public AudioClip exitAudio, caughtAudio;
        AudioSource m_AudioSource;

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
        public IEnumerator EndGame(bool isWin)
        {
            m_AudioSource.clip = (isWin ? exitAudio : caughtAudio);
            m_AudioSource.Play();

            yield return ScreenTransitionController.Instance.ChangeSceneCoroutine<ScreenFadeInTransition, ScreenFadeOutTransition>(isWin ? "ExitScene" : "CaughtScene", 0.5f, 1);

            if (!isWin)
            {
                yield return new WaitForSeconds(1);
                yield return ScreenTransitionController.Instance.ChangeSceneCoroutine<ScreenFadeInTransition, ScreenFadeOutTransition>("GameScene", 0.5f, 1);
            }
        }
    }
}
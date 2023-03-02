using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Structs.Singleton;
using Assets.Scripts.Utils.Transitions;

namespace Assets.Scripts
{
    public class GameOverController : DDOLSingletonMonoBehaviour<GameOverController>
    {
        public AudioClip exitAudio, caughtAudio;
        AudioSource m_AudioSource;
        public bool isWin;

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
        public IEnumerator EndGame(bool isWin)
        {
            this.isWin = isWin;
            m_AudioSource.clip = (isWin ? exitAudio : caughtAudio);
            m_AudioSource.Play();

            yield return ScreenTransitionController.Instance.ChangeSceneCoroutine<ScreenFadeInTransition, ScreenFadeOutTransition>("GameOverScene", 0.5f, 1);
        }
    }
}
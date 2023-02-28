using Assets.Scripts.Structs.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Transitions
{
    public class ScreenTransitionController : DDOLSingletonMonoBehaviour<ScreenTransitionController>
    {
        readonly Dictionary<string, GameObject> m_TransitionPrefabs = new();
        UnityEngine.Canvas canvas;
        public GameObject[] transitionPrefabList;

        private void Start()
        {
            canvas = GetComponentInChildren<UnityEngine.Canvas>();
            Debug.Log(canvas);
            foreach (GameObject prefab in transitionPrefabList)
            {
                m_TransitionPrefabs.Add(prefab.name, prefab);
            }
        }

        public IEnumerator StartTransition<T>(float duration) where T : ScreenTransition => StartTransition<T>(duration, null);
        public IEnumerator StartTransition<T>(float duration, Func<YieldInstruction>? callbackBeforeDestroy) where T : ScreenTransition
        {
            T transition = Instantiate(m_TransitionPrefabs[typeof(T).Name], canvas.transform).GetComponent<T>();
            yield return transition.Run(duration);
            if(callbackBeforeDestroy != null) yield return callbackBeforeDestroy();
            Destroy(transition.gameObject);
        }

        public IEnumerator ChangeSceneCoroutine<TI, TO>(string sceneName, float transitionInDuration, float transitionOutDuration)
             where TI : ScreenTransition
             where TO : ScreenTransition
        {
            yield return StartTransition<TI>(transitionInDuration,
                () => SceneManager.LoadSceneAsync(sceneName)
            );
            yield return StartTransition<TO>(transitionOutDuration);
        }
        public void ChangeScene<TI, TO>(string sceneName, float transitionInDuration, float transitionOutDuration)
             where TI : ScreenTransition
             where TO : ScreenTransition
        {
            StartCoroutine(ChangeSceneCoroutine<TI, TO>(sceneName, transitionInDuration, transitionOutDuration));
        }
    }
}

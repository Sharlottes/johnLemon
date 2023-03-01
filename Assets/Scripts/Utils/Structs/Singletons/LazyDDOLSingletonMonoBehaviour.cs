using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Structs.Singleton
{
    public class LazyDDOLSingletonMonoBehaviour<T> : LazySingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
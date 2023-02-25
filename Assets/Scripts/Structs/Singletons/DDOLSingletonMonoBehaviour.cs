using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Structs.Singleton
{
    public class DDOLSingletonMonoBehaviour<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
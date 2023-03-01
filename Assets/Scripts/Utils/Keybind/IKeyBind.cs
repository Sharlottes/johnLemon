using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public interface IKeyBind
    {
        public KeyCode[] GetKeys();
    }
}
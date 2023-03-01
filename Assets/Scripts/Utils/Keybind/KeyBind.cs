using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public class KeyBind : Conditional<KeyCode[]>, IKeyBind
    {
        public KeyBind(Condit<KeyCode[]> condition) : base(condition) { }
        public KeyBind(KeyCode keys) : base((out KeyCode[] res) => {
            res = new[] { keys };
            return Input.GetKey(keys);
        })
        {
            this.keys = new[] { keys };
        }
        public KeyBind(KeyCode[] keys, Condit<KeyCode[]> condition) : base(condition)
        {
            this.keys = keys;
        }
        public KeyCode[] keys;
        public KeyCode[] GetKeys() => keys;
    }
}
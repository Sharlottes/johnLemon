using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public delegate bool IsKeyPressedDelegate(out KeyCode[] res);
    public class KeyBind
    {
        public IsKeyPressedDelegate isKeyPressed;
        public readonly KeyCode[] keys;

        public KeyBind(IsKeyPressedDelegate isKeyPressed) : this(null, isKeyPressed) { }

        public KeyBind(KeyCode key): this(new[] { key }, (out KeyCode[] res) => {
            res = new[] { key };
            return Input.GetKey(key);
        }) { }

        public KeyBind(KeyCode[] keys, IsKeyPressedDelegate isKeyPressed)
        {
            this.isKeyPressed = isKeyPressed;
            this.keys = keys;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Utils.Keybind
{
    public class BindObject
    {
        public KeyBind bind;
        public Action<KeyCode[]> callback, elseCallback;
        public bool HasCallback => callback != null || elseCallback != null;

        public BindObject(KeyBind bind)
        {
            this.bind = bind;
        }
    }
}
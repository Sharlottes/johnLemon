using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public class BindObject
    {
        public string name;
        public KeyBind bind;
        public Action<KeyCode[], BindObject> callback = (_, __) => { };
        public Action elseCallback = () => { };

        public BindObject(KeyBind bind, string name)
        {
            this.bind = bind;
            this.name = name;
        }
    }
}
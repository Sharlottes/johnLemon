using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public struct BindOptions
    {
        public bool once;
        public string name;
    }

    public class BindObject
    {
        public bool once = false;
        public string name = "";

        public KeyBind bind;
        public Action<KeyCode[], BindObject> callback = (_, __) => { };
        public Action elseCallback = () => { };

        public BindObject(BindOptions options)
        {
            once = options.once;
            name = options.name;
        }
    }
}
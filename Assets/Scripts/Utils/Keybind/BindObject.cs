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

        public static BindOptions defaultObject = new() { once = false, name = "" };
    }

    public class BindObject
    {
        public bool once = false;
        public string name = "";
        public int id;

        public KeyBind bind;
        public Action<KeyCode[], BindObject> callback = (_, __) => { };
        public Action elseCallback = () => { };

        public BindObject(BindOptions options, KeyBind bind)
        {
            once = options.once;
            name = options.name;
            this.bind = bind;
            id = KeyBindManager.Instance.binds.Count;
        }
    }
}
﻿using Assets.Scripts.Structs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public class AndBind : KeyBind
    {
        public AndBind(params KeyCode[] keys) : base(keys, (out KeyCode[] res) =>
        {
            foreach (KeyCode code in keys)
            {
                if (!Input.GetKey(code))
                {
                    res = null;
                    return false;
                }
            }
            res = keys;
            return true;
        })
        { }
        public AndBind(params KeyBind[] binds) : base((out KeyCode[] res) =>
        {
            List<KeyCode> list = new();
            foreach (KeyBind keybind in binds)
            {
                if (keybind.condition(out KeyCode[] bindKeys))
                { 
                    list.AddRange(keybind.GetKeys()); 
                } 
                else {
                    res = null;
                    return false;
                }
            }
            res = list.ToArray();
            return true;
        })
        { }
    }
}
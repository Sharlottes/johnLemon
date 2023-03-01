using Assets.Scripts.Structs;
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
        public AndBind(params Conditional[] binds) : base((out KeyCode[] res) =>
        {
            List<KeyCode> list = new();
            foreach (Conditional cond in binds)
            {
                if (!cond.condition())
                {
                    res = null;
                    return false;
                }
                else if (cond is KeyBind keybind)
                {
                    list.Concat(keybind.GetKeys());
                }
            }
            res = list.ToArray();
            return true;
        })
        { }
    }
}
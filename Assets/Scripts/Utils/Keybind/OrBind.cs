using Assets.Scripts.Structs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public class OrBind : KeyBind
    {
        public OrBind(params KeyCode[] keys) : base(keys, (out KeyCode[] res) =>
        {
            foreach (KeyCode code in keys)
            {
                if (Input.GetKey(code))
                {
                    res = new[] { code };
                    return true;
                }
            }
            res = null;
            return false;
        })
        { }
        public OrBind(params Conditional[] binds) : base((out KeyCode[] res) =>
        {
            List<KeyCode> list = new();
            foreach (Conditional cond in binds)
            {
                if (cond.condition())
                {
                    res = cond is KeyBind kb ? kb.GetKeys() : null;
                    return true;
                }
                else if (cond is KeyBind keybind)
                {
                    list.Concat(keybind.GetKeys());
                }
            }
            res = null;
            return false;
        })
        { }
    }
}
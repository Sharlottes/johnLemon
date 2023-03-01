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
            List<KeyCode> list = new();
            foreach (KeyCode code in keys)
            {
                if (Input.GetKey(code)) list.Add(code);
            }
            res = list.ToArray();
            return list.Any();
        })
        { }
        public OrBind(params KeyBind[] binds) : base((out KeyCode[] res) =>
        {
            List<KeyCode> list = new();
            string str = "";
            foreach (KeyCode code in list)
            {
                str += $"{code} ,";
            }
            foreach (KeyBind keybind in binds)
            {
                if (keybind.isKeyPressed(out KeyCode[] bindKeys))
                {
                    list.AddRange(bindKeys);
                }
            }
            res = list.ToArray();
            return list.Any();
        })
        { }
    }
}
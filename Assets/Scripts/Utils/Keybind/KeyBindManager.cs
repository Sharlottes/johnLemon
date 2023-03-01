
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Structs.Singleton;

namespace Assets.Scripts.Utils.Keybind
{
    public enum BindType
    {
        And, Or, None
    }

    public class KeyBindManager : LazyDDOLSingletonMonoBehaviour<KeyBindManager>
    {
        BindObject lastBind;
        List<BindObject> binds = new();

        void Update()
        {
            foreach (BindObject bind in binds)
            {
                RunBind(bind);
            }
            RunBind(lastBind);
        }

        void RunBind(BindObject bind)
        {

            bool isKeyDown = bind.bind.condition(out KeyCode[] keyCodes);
            if (isKeyDown) bind.callback?.Invoke(keyCodes);
            else bind.elseCallback?.Invoke(keyCodes);
        }

        void UpdateLastBind()
        {
            binds.Add(lastBind);
            lastBind = null;
        }

        public KeyBindManager And(params KeyCode[] keyCodes)
        {
            if (lastBind.HasCallback) UpdateLastBind();

            if (lastBind == null) lastBind = new(new AndBind(keyCodes));
            else lastBind.bind = new AndBind(lastBind.bind, new AndBind(keyCodes));

            return this;
        }

        public KeyBindManager Or(params KeyCode[] keyCodes)
        {
            if (lastBind.HasCallback) UpdateLastBind();

            if (lastBind == null) lastBind = new(new OrBind(keyCodes));
            else lastBind.bind = new OrBind(lastBind.bind, new OrBind(keyCodes));

            return this;
        }

        public KeyBindManager Then(Action<KeyCode[]> callback)
        {
            lastBind.callback = callback;
            return this;
        }

        public KeyBindManager Else(Action<KeyCode[]> elseCallback)
        {
            lastBind.elseCallback = elseCallback;
            return this;
        }
    }
}
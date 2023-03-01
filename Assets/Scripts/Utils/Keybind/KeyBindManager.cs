
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Structs.Singleton;

namespace Assets.Scripts.Utils.Keybind
{
    public class KeyBindManager : LazyDDOLSingletonMonoBehaviour<KeyBindManager>
    {
        BindObject lastBind;
        readonly List<BindObject> binds = new();

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
            if (bind.bind.condition(out KeyCode[] keyCodes)) 
                bind.callback?.Invoke(keyCodes, bind);
            else bind.elseCallback?.Invoke();
        }

        void UpdateLastBind()
        {
            binds.Add(lastBind);
            lastBind = null;
        }

        public KeyBindManager Bind()
        {
            if (lastBind != null) UpdateLastBind();
            return this;
        }
        public KeyBindManager Is(KeyCode keyCode) => Is("", keyCode);
        public KeyBindManager Is(string name, KeyCode keyCode) => Is(BindType.NONE, name, keyCode);
        public KeyBindManager Is(BindType type, KeyCode keyCode) => Is(type, "", keyCode);
        public KeyBindManager Is(BindType type, string name, KeyCode keyCode)
        {
            if (lastBind == null) lastBind = new(new KeyBind(keyCode), name);
            else lastBind.bind = type switch
            {
                BindType.AND => new AndBind(lastBind.bind, new KeyBind(keyCode)),
                BindType.OR => new OrBind(lastBind.bind, new KeyBind(keyCode)),
                _ => throw new NotImplementedException()
            };

            return this;
        }

        public KeyBindManager And(params KeyCode[] keyCodes) => And("", keyCodes);
        public KeyBindManager And(string name, params KeyCode[] keyCodes) => And(BindType.NONE, name, keyCodes);
        public KeyBindManager And(BindType type, params KeyCode[] keyCodes) => And(type, "", keyCodes);
        public KeyBindManager And(BindType type, string name, params KeyCode[] keyCodes)
        {
            if (lastBind == null) lastBind = new(new AndBind(keyCodes), name);
            else lastBind.bind = type switch
            {
                BindType.AND => new AndBind(lastBind.bind, new AndBind(keyCodes)),
                BindType.OR => new OrBind(lastBind.bind, new AndBind(keyCodes)),
                _ => throw new NotImplementedException()
            };

            return this;
        }

        public KeyBindManager Or(params KeyCode[] keyCodes) => Or("", keyCodes);
        public KeyBindManager Or(string name, params KeyCode[] keyCodes) => Or(BindType.NONE, name, keyCodes);
        public KeyBindManager Or(BindType type, params KeyCode[] keyCodes) => Or(type, "", keyCodes);
        public KeyBindManager Or(BindType type, string name = "", params KeyCode[] keyCodes)
        {
            if (lastBind == null) lastBind = new(new OrBind(keyCodes), name);
            else lastBind.bind = type switch
            {
                BindType.AND => new AndBind(lastBind.bind, new OrBind(keyCodes)),
                BindType.OR => new OrBind(lastBind.bind, new OrBind(keyCodes)),
                _ => throw new NotImplementedException()
            };

            return this;
        }
        public KeyBindManager Then(Action callback) => Then((codes, bind) => callback());
        public KeyBindManager Then(Action<BindObject> callback) => Then((codes, bind) => callback(bind));
        public KeyBindManager Then(Action<KeyCode[]> callback) => Then((codes, bind) => callback(codes));
        public KeyBindManager Then(Action<KeyCode[], BindObject> callback)
        {
            lastBind.callback = callback;
            return this;
        }

        public KeyBindManager Else(Action elseCallback)
        {
            lastBind.elseCallback = elseCallback;
            return this;
        }
    }
}
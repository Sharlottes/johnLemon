
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
            if (bind.bind.isKeyPressed(out KeyCode[] keyCodes)) 
                bind.callback(keyCodes, bind);
            else bind.elseCallback();
        }

        void UpdateLastBind()
        {
            binds.Add(lastBind);
            lastBind = null;
        }

        T CreateKeyBind<T>(params KeyCode[] keyCodes) where T : KeyBind
        {
           return typeof(T).Name switch
            {
                "AndBind" => new AndBind(keyCodes),
                "OrBind" =>  new OrBind(keyCodes),
                "KeyBind" => new KeyBind(keyCodes[0]),
                _ => throw new NotImplementedException()
            } as T;
        }

        public KeyBindManager Bind()
        {
            if (lastBind != null) UpdateLastBind();
            return this;
        }


        public KeyBindManager Is(params KeyCode[] keyCodes) => Is<KeyBind>(keyCodes);
        public KeyBindManager Is<T>(params KeyCode[] keyCodes) where T : KeyBind => Is<T>("", keyCodes);
        public KeyBindManager Is(string name, params KeyCode[] keyCodes) => Is<KeyBind>(name, keyCodes);
        public KeyBindManager Is<T>(string name, params KeyCode[] keyCodes) where T : KeyBind
        {
            T bind = CreateKeyBind<T>(keyCodes);
            if (lastBind == null) lastBind = new(bind, name);
            else lastBind.bind = bind;

            return this;
        }


        public KeyBindManager And(params KeyCode[] keyCodes) => And<KeyBind>(keyCodes);
        public KeyBindManager And<T>(params KeyCode[] keyCodes) where T : KeyBind => And<T>("", keyCodes);
        public KeyBindManager And(string name, params KeyCode[] keyCodes) => And<KeyBind>(name, keyCodes);
        public KeyBindManager And<T>(string name, params KeyCode[] keyCodes) where T : KeyBind
        {
            T bind = CreateKeyBind<T>(keyCodes);
            if (lastBind == null) lastBind = new(bind, name);
            else lastBind.bind = new AndBind(lastBind.bind, bind);

            return this;
        }

        public KeyBindManager Or(params KeyCode[] keyCodes) => Or<KeyBind>(keyCodes);
        public KeyBindManager Or<T>(params KeyCode[] keyCodes) where T : KeyBind => Or<T>("", keyCodes);
        public KeyBindManager Or(string name, params KeyCode[] keyCodes) => Or<KeyBind>(name, keyCodes);
        public KeyBindManager Or<T>(string name, params KeyCode[] keyCodes) where T : KeyBind
        {
            T bind = CreateKeyBind<T>(keyCodes);
            if (lastBind == null) lastBind = new(bind, name);
            else lastBind.bind = new OrBind(lastBind.bind, bind);

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
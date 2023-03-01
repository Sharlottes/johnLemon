
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
        }

        void RunBind(BindObject bind)
        {
            if (bind.bind.isKeyPressed(out KeyCode[] keyCodes)) 
                bind.callback(keyCodes, bind);
            else bind.elseCallback();
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

        public KeyBindManager Bind() => Bind(new() { name = "", once = false });
        public KeyBindManager Bind(BindOptions options)
        {
            binds.Add(lastBind = new BindObject(options));

            return this;
        }


        public KeyBindManager Is(params KeyCode[] keyCodes) => Is<KeyBind>(keyCodes);
        public KeyBindManager Is<T>(params KeyCode[] keyCodes) where T : KeyBind
        {
            lastBind.bind = CreateKeyBind<T>(keyCodes);

            return this;
        }


        public KeyBindManager And(params KeyCode[] keyCodes) => And<KeyBind>(keyCodes);
        public KeyBindManager And<T>(params KeyCode[] keyCodes) where T : KeyBind
        {
            lastBind.bind = new AndBind(lastBind.bind, CreateKeyBind<T>(keyCodes));

            return this;
        }

        public KeyBindManager Or(params KeyCode[] keyCodes) => Or<KeyBind>(keyCodes);
        public KeyBindManager Or<T>(params KeyCode[] keyCodes) where T : KeyBind
        {
            lastBind.bind = new OrBind(lastBind.bind, CreateKeyBind<T>(keyCodes));

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

using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Structs.Singleton;
using System.Linq;

namespace Assets.Scripts.Utils.Keybind
{
    public class KeyBindManager : LazyDDOLSingletonMonoBehaviour<KeyBindManager>
    {
        public readonly List<BindObject> binds = new();
        BindObject LastBind => binds.LastOrDefault();

        void Update()
        {
            for (int i = 0; i < binds.Count; i++)
            {
                BindObject bind = binds[i];
                RunBind(bind);
            }
            if(LastBind != null) RunBind(LastBind);
        }

        void RunBind(BindObject bind)
        {
            if (bind.bind.isKeyPressed(out KeyCode[] keyCodes))
            {
                bind.callback(keyCodes, bind);
                if(bind.once) binds.Remove(bind);
            }
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

        public KeyBindManager Bind(params KeyCode[] keyCodes) => Bind(BindOptions.defaultObject, keyCodes);
        public KeyBindManager Bind<T>(params KeyCode[] keyCodes) where T : KeyBind => Bind<T>(BindOptions.defaultObject, keyCodes);
        public KeyBindManager Bind(BindOptions options, params KeyCode[] keyCodes) => Bind<KeyBind>(options, keyCodes);
        public KeyBindManager Bind<T>(BindOptions options, params KeyCode[] keyCodes) where T : KeyBind
        {
            binds.Add(new BindObject(options, CreateKeyBind<T>(keyCodes)));
            return this;
        }

        public KeyBindManager GetID(out int id)
        {
            id = LastBind.id;
            return this;
        }
        public KeyBindManager UnBind(int id)
        {
            int index = binds.FindIndex(bind => bind.id == id);
            if(index != -1) binds.RemoveAt(index);
            return this;
        }

        public KeyBindManager And(params KeyCode[] keyCodes) => And<KeyBind>(keyCodes);
        public KeyBindManager And<T>(params KeyCode[] keyCodes) where T : KeyBind
        {
            LastBind.bind = new AndBind(LastBind.bind, CreateKeyBind<T>(keyCodes));
            return this;
        }

        public KeyBindManager Or(params KeyCode[] keyCodes) => Or<KeyBind>(keyCodes);
        public KeyBindManager Or<T>(params KeyCode[] keyCodes) where T : KeyBind
        {
            LastBind.bind = new OrBind(LastBind.bind, CreateKeyBind<T>(keyCodes));
            return this;
        }

        public KeyBindManager Then(Action callback) => Then((codes, bind) => callback());
        public KeyBindManager Then(Action<BindObject> callback) => Then((codes, bind) => callback(bind));
        public KeyBindManager Then(Action<KeyCode[]> callback) => Then((codes, bind) => callback(codes));
        public KeyBindManager Then(Action<KeyCode[], BindObject> callback)
        {
            LastBind.callback = callback;
            return this;
        }

        public KeyBindManager Else(Action elseCallback)
        {
            LastBind.elseCallback = elseCallback;
            return this;
        }
    }
}
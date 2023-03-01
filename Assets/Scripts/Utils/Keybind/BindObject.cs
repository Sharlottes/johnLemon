using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


namespace Assets.Scripts.Utils.Keybind
{
    public interface ICallbackInvoker
    {
        void Run();
    }
    public interface IKeyCodesCallbackInvoker : ICallbackInvoker
    {
        void Run(KeyCode[] codes);
    }
    public interface IKeyCodesBindCallbackInvoker : ICallbackInvoker
    {
        void Run(KeyCode[] codes, BindObject bind);
    }
    public class BindObject
    {
        public string name;
        public KeyBind bind;
        public Action<KeyCode[], BindObject> callback;
        public Action elseCallback;
        public bool HasCallback => callback != null || elseCallback != null;

        public BindObject(KeyBind bind, string name)
        {
            this.bind = bind;
            this.name = name;
        }
    }
}
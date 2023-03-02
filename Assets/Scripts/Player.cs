using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Structs.Singleton;
using Assets.Scripts.Utils.Keybind;

namespace Assets.Scripts
{
    public class Player : SingletonMonoBehaviour<Player>
    {
        int itemUseKeyBindId;
        private void Awake()
        {
            KeyBindManager.Instance

                .Bind(KeyCode.E)
                    .Then(() =>
                    {
                        //TODO: 아이템 사용
                    })
                    .GetID(out itemUseKeyBindId);
        }

        private void OnDestroy()
        {
            KeyBindManager.Instance.UnBind(itemUseKeyBindId);
        }
    }
}
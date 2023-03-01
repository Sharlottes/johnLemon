using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Utils.Keybind
{
    public class KeyUtils
    {

        public static bool IsEveryKeysDown(out KeyCode[] codes, params KeyCode[] keyCodes)
        {
            codes = new KeyCode[] { };
            bool isKeyDown = true;
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    isKeyDown = false;
                    break;
                }
            }
            if (isKeyDown) codes = keyCodes;
            return isKeyDown;
        }

        public static bool IsSomeKeyDown(out KeyCode? codes, params KeyCode[] keyCodes)
        {
            codes = null;

            bool isKeyDown = false;
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    isKeyDown = true;
                    codes = keyCode;
                    break;
                }
            }
            return isKeyDown;
        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    internal class Global
    {
        public static IEnumerator DummyCoroutine()
        {
            yield return null;
        }
    }
}

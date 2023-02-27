using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Structs
{
    internal class SingleCoroutineController
    {
        Coroutine m_CurrentCoroutine;
        Func<IEnumerator> m_CoroutineInvoker;
        readonly MonoBehaviour m_MB;

        public SingleCoroutineController(MonoBehaviour MB, Func<IEnumerator> coroutineInvoker)
        {
            m_MB = MB;
            m_CoroutineInvoker = coroutineInvoker;
        }


        public void Start(bool restartOnDuplicated = false)
            => Start(m_CoroutineInvoker, restartOnDuplicated);

        public void Start(IEnumerator coroutine, bool restartOnDuplicated = false)
            => Start(() => coroutine, restartOnDuplicated);

        public void Start(Func<IEnumerator> coroutineInvoker, bool restartOnDuplicated = false)
        {
            if (restartOnDuplicated) Stop();
            m_CurrentCoroutine ??= m_MB.StartCoroutine(coroutineInvoker());
        }
        public void Stop()
        {
            if (m_CurrentCoroutine != null)
            {
                m_MB.StopCoroutine(m_CurrentCoroutine);
                m_CurrentCoroutine = null;
            }
        }
    }
}

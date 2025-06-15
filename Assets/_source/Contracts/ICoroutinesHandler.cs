using System.Collections;
using UnityEngine;

namespace GameContracts
{
    public interface ICoroutinesHandler
    {
        public Coroutine StartCoroutine(string methodName);
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(Coroutine coroutine);
        public void ActivateCoroutine(ref Coroutine coroutineReference, IEnumerator coroutine);
    }
}
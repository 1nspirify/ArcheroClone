using System.Collections;
using UnityEngine;

namespace _Project.Dev.Runtime.Utilities.CoroutinesManagment
{
    public interface ICoroutinesPerformer
    {
        public Coroutine StartPerform(IEnumerator coroutineFunction);
        public void StopPerform(Coroutine coroutine);
    }
}
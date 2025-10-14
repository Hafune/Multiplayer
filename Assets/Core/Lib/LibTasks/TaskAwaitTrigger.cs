using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskAwaitTrigger : MonoConstruct, IMyTask
    {
        private Action _onComplete;
        
        public bool InProgress { get; private set; }
        public int _count;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            if (_count > 0)
            {
                onComplete?.Invoke();
                return;
            }
            
            _onComplete = onComplete;
            InProgress = true;
        }

        private void OnTriggerEnter2D(Collider2D _)
        {
            _count++;
            if (!InProgress)
                return;

            InProgress = false;
            _onComplete?.Invoke();            
        }

        private void OnTriggerExit2D(Collider2D _) => _count--;
    }
}
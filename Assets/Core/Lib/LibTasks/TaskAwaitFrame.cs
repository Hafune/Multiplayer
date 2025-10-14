using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskAwaitFrame : MonoConstruct, IMyTask
    {
        private Action _onComplete;
        
        public bool InProgress { get; private set; }

        private void Awake() => enabled = false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            enabled = true;
            _onComplete = onComplete;
        }

        private void Update()
        {
            enabled = false;
            _onComplete?.Invoke();
        }
    }
}
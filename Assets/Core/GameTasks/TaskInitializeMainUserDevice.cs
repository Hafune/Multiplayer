using System;
using Core.Lib;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskInitializeMainUserDevice : MonoConstruct, IMyTask
    {
        public bool InProgress { get; private set; }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            context.Resolve<InputService>().InitializeRootUserWithLastDevice();
            onComplete?.Invoke();
        }
    }
}
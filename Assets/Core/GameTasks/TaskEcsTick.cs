using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskEcsTick : MonoConstruct, IMyTask
    {
        public bool InProgress { get; private set; }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            context.Resolve<EcsEngine>().Tick();
            onComplete?.Invoke();
        }
    }
}
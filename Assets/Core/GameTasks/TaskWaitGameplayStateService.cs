using System;
using System.Collections;
using Core.Lib;
using Core.Services;
using Lib;

namespace Core.Tasks
{
    public class TaskWaitGameplayStateService : MonoConstruct, IMyTask
    {
        public bool InProgress { get; private set; }
        private Payload _selfPayload;

        private IEnumerator Wait(Action onComplete)
        {
            var playerStateService = context.Resolve<GameplayStateService>();
            while (!playerStateService.IsActiveState)
                yield return null;
            
            InProgress = false;
            onComplete?.Invoke();
        }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            InProgress = true;
            StartCoroutine(Wait(onComplete));
        }

        private void OnDestroy() => _selfPayload?.Dispose();
    }
}
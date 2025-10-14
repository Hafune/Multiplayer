using System;
using Core.Lib;
using Lib;

namespace Core.Tasks
{
    [Obsolete("Доработать")]
    public class TaskWaitEntityDeath : MonoConstruct, IMyTask
    {
        private Action _onComplete;
        public bool InProgress { get; private set; }

        private void Awake() => throw new Exception("Доработать");

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            InProgress = true;
            _onComplete = onComplete;
            // payload.Get(CommonPayloadKeys.ConvertToEntity).BeforeEntityDeleted += EntityRemoved;
        }

        private void EntityRemoved(ConvertToEntity entityRef)
        {
            entityRef.BeforeEntityDeleted -= EntityRemoved;
            InProgress = false;
            _onComplete?.Invoke();
        }
    }
}
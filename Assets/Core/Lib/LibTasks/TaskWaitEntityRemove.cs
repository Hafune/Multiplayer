using System;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    [Obsolete("Доработать")]
    public class TaskWaitEntityRemove : MonoConstruct, IMyTask
    {
        private ConvertToEntity _convertToEntity;
        private Action _onComplete;

        public bool InProgress { get; private set; }

        private void Awake() => throw new Exception("Доработать");

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {            
            _onComplete = onComplete;

            if (_convertToEntity.RawEntity == -1)
                onComplete?.Invoke();
            else
            {
                InProgress = true;
                _convertToEntity.BeforeEntityDeleted += OnRemove;
            }
        }

        private void OnRemove(ConvertToEntity convertToEntity)
        {
            InProgress = false;
            convertToEntity.BeforeEntityDeleted -= OnRemove;
            _onComplete?.Invoke();
        }
    }
}
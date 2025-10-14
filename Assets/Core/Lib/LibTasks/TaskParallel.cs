using System;
using System.Collections.Generic;
using Core.Lib;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core
{
    public class TaskParallel : MonoConstruct, IMyTask
    {
        [SerializeField] private bool _includeInactive;
        private IMyTask[] _tasks;
        private int _completedCount;

        [CanBeNull] private Action _onComplete;

        public bool InProgress { get; private set; }

        private void Awake()
        {
            List<IMyTask> iTasks = new(transform.childCount);
            transform.ForEachSelfChildren<IMyTask>(iTasks.Add, _includeInactive);
            _tasks = iTasks.ToArray();
        }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            if (InProgress)
                Debug.LogError("Новый вызов таски до её завершения");

            InProgress = true;
            _onComplete = onComplete;
            _completedCount = 0;

            for (int i = 0, iMax = _tasks.Length; i < iMax; i++)
                _tasks[i].Begin(payload, Complete);
        }

        private void Complete()
        {
            if (++_completedCount < _tasks.Length)
                return;

            InProgress = false;
            _onComplete?.Invoke();
        }

        public void Cancel()
        {
#if UNITY_EDITOR
            if (!InProgress)
                Debug.LogError($"{nameof(Cancel)} вызван вне прогресса");
#endif
            InProgress = false;
            for (int i = 0, iMax = _tasks.Length; i < iMax; i++)
                _tasks[i].Cancel();
        }
    }
}
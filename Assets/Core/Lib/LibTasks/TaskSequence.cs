using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSequence : MonoConstruct, IMyTask
    {
        private Payload _payload;
        private readonly List<IMyTask> _tasks = new();
        private int _index;
        private IMyTask _currentTask;
        private Action _onComplete;

        public bool InProgress { get; protected set; }

        public virtual void Begin(
            Payload payload,
            Action onComplete = null)
        {
            if (InProgress && Application.isPlaying)
                Debug.LogError("Новый вызов таски до её завершения");

            InProgress = true;
            _payload = payload;
            _onComplete = onComplete;
            _index = -1;
            _tasks.Clear();
            transform.ForEachSelfChildren<IMyTask>(_tasks.Add);

            Next();
        }

        private void Next()
        {
#if UNITY_EDITOR
            if (!InProgress)
                Debug.LogError("Калбек когда прогресс уже остановлен!");
#endif
            if (_tasks.Count > ++_index)
            {
                _currentTask = _tasks[_index];
                _currentTask.Begin(_payload, Next);
            }
            else
            {
                InProgress = false;
                _onComplete?.Invoke();
            }
        }

        public void Cancel()
        {
#if UNITY_EDITOR
            if (!InProgress)
                Debug.LogError($"{nameof(Cancel)} вызван вне прогресса");
#endif
            InProgress = false;
            _currentTask?.Cancel();
            _currentTask = null;
        }
    }
}
using System;
using System.Collections;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskAwaitSecond : MonoConstruct, IMyTask
    {
        [SerializeField] [Min(0)] private float _time;
        private WaitForSeconds _wait;
        private Coroutine _coroutine;

        public bool InProgress { get; private set; }

        private void Awake() => _wait = new WaitForSeconds(_time);

        private void OnDisable()
        {
            if (_coroutine == null)
                return;
            
            StopCoroutine(_coroutine);
            InProgress = false;
        }

        public void Begin(
            Payload payload,
            Action onComplete = null)
            => _coroutine = StartCoroutine(StartSpawn(onComplete));

        private IEnumerator StartSpawn(Action onComplete)
        {
            InProgress = true;
            yield return _wait;
            InProgress = false;
            _coroutine = null;
            onComplete?.Invoke();
        }
    }
}
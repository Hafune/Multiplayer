using System;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class TaskOutside : MonoConstruct, IMyTask
    {
        [SerializeField, TypeCheck(typeof(IMyTask))]
        private MonoBehaviour _outsideTask;

        [SerializeField] private bool _waitForCompletion;

        public bool InProgress => false;

        private void Awake() => Assert.IsNotNull(_outsideTask);

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            if (_waitForCompletion)
            {
                ((IMyTask)_outsideTask).Begin(payload, onComplete);
            }
            else
            {
                ((IMyTask)_outsideTask).Begin(payload);
                onComplete?.Invoke();
            }
        }
    }
}
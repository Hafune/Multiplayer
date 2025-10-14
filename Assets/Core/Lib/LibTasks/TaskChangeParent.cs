using System;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskChangeParent : MonoConstruct, IMyTask
    {
        [SerializeField] private Transform _nextParent;
        [SerializeField] private Transform _target;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _target.parent = _nextParent;
            onComplete?.Invoke();
        }
    }
}
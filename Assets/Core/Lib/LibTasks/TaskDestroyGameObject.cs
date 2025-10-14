using System;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskDestroyGameObject : MonoConstruct, IMyTask
    {
        [SerializeField] private GameObject _target;
        public bool InProgress { get; }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            Destroy(_target);
            onComplete?.Invoke();
        }
    }
}
using System;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSetWorldPosition : MonoConstruct, IMyTask
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _position;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _target.position = _position;
            onComplete?.Invoke();
        }
    }
}
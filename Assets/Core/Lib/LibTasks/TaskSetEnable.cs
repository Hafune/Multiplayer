using System;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSetEnable : MonoConstruct, IMyTask
    {
        [SerializeField] private Behaviour _behaviour;
        [SerializeField] private bool _enabled;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _behaviour.enabled = _enabled;
            onComplete?.Invoke();
        }
    }
}
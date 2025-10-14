using System;
using Core.Lib;
using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskSetActive : MonoConstruct, IMyTask
    {
        [SerializeField] private bool _activeState = true;
        [CanBeNull, SerializeField] private GameObject _outsideTarget;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            if (_outsideTarget)
                _outsideTarget.SetActive(_activeState);
            else
                gameObject.SetActive(_activeState);

            onComplete?.Invoke();
        }
    }
}
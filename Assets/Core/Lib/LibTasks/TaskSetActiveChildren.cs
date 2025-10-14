using System;
using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSetActiveChildren : MonoConstruct, IMyTask
    {
        [SerializeField] private bool _activeState = true;
        [CanBeNull, SerializeField] private GameObject _outsideTarget;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            if (_outsideTarget)
                foreach (Transform child in _outsideTarget.transform)
                    child.gameObject.SetActive(_activeState);
            else
                foreach (Transform child in transform)
                    child.gameObject.SetActive(_activeState);

            onComplete?.Invoke();
        }
    }
}
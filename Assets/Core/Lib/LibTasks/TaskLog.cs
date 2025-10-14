using System;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskLog : MonoConstruct, IMyTask
    {
        [SerializeField] private string _message;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            Debug.Log(_message);
            onComplete?.Invoke();
        }
    }
}
using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Tasks
{
    [Obsolete(@"Использовать только для одноразовой логики (пример туториалы, катсцены)
Не использовать для кор логики!!! 
")]
    public class TaskUnityEventForSceneAction : MonoConstruct, IMyTask
    {
        [SerializeField] private UnityEvent _event;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _event.Invoke();
            onComplete?.Invoke();
        }
    }
}
using System;
using Core.Lib;
using Core.Services;
using Lib;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskSetPlayerPosition : MonoConstruct, IMyTask
    {
        [SerializeField] private Entrances_Row _entrance;
        
        public bool InProgress { get; private set; }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            context.Resolve<GameplayStateService>().SetPlayerPosition(_entrance);
            onComplete?.Invoke();
        }
    }
}
using System;
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    [Obsolete("Не работает, обновить если понадобится!")]
    public class TaskAddStartActionEventToPlayer : MonoConstruct, IMyTask
    {
        // [SerializeField] private ActionEnum _actionEnum;
        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            // incomingContext
            //     .Resolve<ActionSystemsService>()
            //     .AddStartEvent(_actionEnum, incomingContext.Resolve<EcsWorld>().Filter<Player1UniqueTag>().End().GetFirst());
            //
            // onComplete?.Invoke();
        }
    }
}
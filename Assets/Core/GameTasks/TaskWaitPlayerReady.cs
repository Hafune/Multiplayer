using System;
using System.Collections;
using Core.Components;
using Core.Lib;
using Core.Services;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskWaitPlayerReady : MonoConstruct, IMyTask
    {
        public bool InProgress { get; private set; }

        public void Begin(
            Payload payload,
            Action onComplete = null)
            => StartCoroutine(Wait(onComplete));

        private IEnumerator Wait(Action onComplete)
        {
            var filter = context.Resolve<EcsWorld>().Filter<Player1UniqueTag>().Exc<EventInit>().End();
            InProgress = true;

            while (filter.GetEntitiesCount() == 0)
                yield return null;

            InProgress = false;

            onComplete?.Invoke();
        }
    }
}
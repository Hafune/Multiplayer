using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class RemoveByTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventRemoveTimer
            >> _startFilter;

        private readonly EcsFilterInject<
            Inc<
                RemoveTimerComponent
            >,
            Exc<
                EventWaitInit
            >> _progressFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _startFilter.Value)
            {
                ref var timer = ref _pools.RemoveTimer.GetOrInitialize(i);
                timer.startTime = Time.time;
                timer.duration = _pools.EventRemoveTimer.Get(i).maxTime;
                _pools.EventRemoveTimer.Del(i);
            }

            foreach (var i in _progressFilter.Value)
            {
                ref var timer = ref _pools.RemoveTimer.Get(i);
                if (Time.time - timer.startTime < timer.duration)
                    continue;

                _pools.EventRemoveEntity.AddIfNotExist(i);
            }
        }
    }
}
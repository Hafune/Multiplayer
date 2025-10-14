using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class LifetimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                LifetimeComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            ref var timeComponent = ref _pools.Lifetime.Get(entity);
            timeComponent.currentTime += Time.deltaTime;

            if (timeComponent.currentTime < timeComponent.maxTime)
                return;

            _pools.EventStartActionDeath.AddIfNotExist(entity);
            _pools.Lifetime.Del(entity);
        }
    }
}
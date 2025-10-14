using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class CalculateMoveDestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                CalculateMoveDestinationTag,
                MoveDestinationComponent,
                MoveDesiredPositionComponent
            >> _filter;

        private readonly ComponentPools _pools;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var dest = ref _pools.MoveDestination.Get(i);

                if (dest.nextCalculateTime - Time.time >= 0)
                    continue;

                ref var endpoint = ref _pools.MoveDesiredPosition.Get(i);
                dest.position = endpoint.position;
                dest.distanceToSuccess = endpoint.distanceToSuccess;
            }
        }
    }
}
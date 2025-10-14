using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Systems
{
    public class FindTargetSystem<T> : IEcsRunSystem, IDisposable where T : struct
    {
        private readonly EcsFilterInject<
            Inc<
                FindTargetComponent<T>,
                PositionComponent
            >,
            Exc<
                TargetComponent
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                TargetComponent,
                TargetResetByDelayComponent
            >> _dropTargetFilter;

        private readonly EcsFilterInject<
            Inc<
                T,
                PositionComponent
            >,
            Exc<
                InProgressTag<ActionDeathComponent>
            >> _targetsFilter;

        private readonly ComponentPools _pools;
        private readonly RelationFunctions<AimComponent, TargetComponent> _relationFunctions;

        private NativeArray<int> _entities;
        private NativeArray<float2> _entityPositions;
        private int _entitiesCount;

        private NativeArray<int> _possibleTargets;
        private NativeArray<float2> _targetPositions;
        private int _targetsCount;

        private NativeArray<int> _resultTargets;

        public FindTargetSystem(Context context)
        {
            _relationFunctions = new(context);
            _entities = new NativeArray<int>(short.MaxValue, Allocator.Persistent);
            _entityPositions = new NativeArray<float2>(short.MaxValue, Allocator.Persistent);
            _entitiesCount = 0;
            _possibleTargets = new NativeArray<int>(short.MaxValue, Allocator.Persistent);
            _resultTargets = new NativeArray<int>(short.MaxValue, Allocator.Persistent);
            _targetPositions = new NativeArray<float2>(short.MaxValue, Allocator.Persistent);
            _targetsCount = 0;
            context.Resolve<DisposableServices>().Add(this);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _dropTargetFilter.Value)
            {
                ref var delay = ref _pools.TargetResetByDelay.Get(i);

                if (delay.startTime + delay.delay > Time.time)
                    continue;

                delay.startTime = Time.time;
                _relationFunctions.DisconnectChild(i);
            }

            _entitiesCount = _filter.Value.GetEntitiesCount();
            _targetsCount = _targetsFilter.Value.GetEntitiesCount();
            
            if (_entitiesCount == 0 || _targetsCount == 0)
                return;

            int index = 0;
            foreach (var i in _filter.Value)
            {
                _entities[index] = i;
                _entityPositions[index] = ((float3)_pools.Position.Get(i).transform.position).xy;
                index++;
            }

            index = 0;
            foreach (var i in _targetsFilter.Value)
            {
                _possibleTargets[index] = i;
                _targetPositions[index] = ((float3)_pools.Position.Get(i).transform.position).xy;
                index++;
            }

            var job = new FindTargetJob
            {
                entityPositions = _entityPositions,
                targetPositions = _targetPositions,
                possibleTargets = _possibleTargets,
                targetsCount = _targetsCount,
                resultTargets = _resultTargets,
            };

            job.Schedule(_entitiesCount, 0).Complete();

            for (int i = 0; i < _entitiesCount; i++)
            {
                int nearestEntity = _resultTargets[i];

                if (nearestEntity == -1)
                    continue;

                _relationFunctions.Connect(nearestEntity, _entities[i]);
            }
        }

        [BurstCompile]
        private struct FindTargetJob : IJobParallelFor
        {
            [ReadOnly, NativeDisableContainerSafetyRestriction, NativeDisableParallelForRestriction]
            public NativeArray<float2> entityPositions;

            [ReadOnly, NativeDisableContainerSafetyRestriction, NativeDisableParallelForRestriction]
            public NativeArray<float2> targetPositions;

            [ReadOnly, NativeDisableContainerSafetyRestriction, NativeDisableParallelForRestriction]
            public NativeArray<int> possibleTargets;

            [ReadOnly] public int targetsCount;

            [WriteOnly, NativeDisableContainerSafetyRestriction, NativeDisableParallelForRestriction]
            public NativeArray<int> resultTargets;

            public void Execute(int index)
            {
                float minCost = float.PositiveInfinity;
                int nearestEntity = -1;

                for (int i = 0; i < targetsCount; i++)
                {
                    float cost = math.lengthsq(targetPositions[i] - entityPositions[index]);

                    if (cost > minCost)
                        continue;

                    minCost = cost;
                    nearestEntity = possibleTargets[i];
                }

                resultTargets[index] = nearestEntity;
            }
        }

        public void Dispose()
        {
            _entities.Dispose();
            _entityPositions.Dispose();
            _possibleTargets.Dispose();
            _targetPositions.Dispose();
            _resultTargets.Dispose();
        }
    }
}
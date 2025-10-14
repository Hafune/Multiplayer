using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class ActionReviveSystem : AbstractActionSystem<ActionReviveComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionCurrentComponent,
                ActionReviveComponent,
                PositionComponent,
                EventActionStart<ActionReviveComponent>,
                AnimatorComponent
            >,
            Exc<
                InProgressTag<ActionReviveComponent>
            >> _startFilter;

        private readonly EcsFilterInject<
            Inc<
                InvulnerabilityLifetimeComponent
            >> _invulnerabilityFilter;

        private readonly EcsFilterInject<
            Inc<
                EventGlobalDispatch<ActionReviveComponent>
            >> _eventDispatchFilter;

        private const int _invulnerabilityMaxLifetime = 3;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _startFilter.Value)
                _pools.EventGlobalDispatchActionRevive.Del(i);

            foreach (var i in _startFilter.Value)
            {
                BeginActionProgress(i, _actionPool.Value.Get(i).logic);
                _pools.EventGlobalDispatchActionRevive.Add(i);
                _pools.HitPointValue.Get(i).value = _pools.HitPointMaxValue.Get(i).value;
                _pools.EventUpdatedHitPointValue.AddIfNotExist(i);

                ref var invulnerability = ref _pools.InvulnerabilityLifetime.GetOrInitialize(i);
                invulnerability.startTime = Time.time;
            }

            foreach (var i in _invulnerabilityFilter.Value)
                UpdateLifetime(i);

            CleanEventStart();
        }

        private void UpdateLifetime(int entity)
        {
            if (Time.time - _pools.InvulnerabilityLifetime.Get(entity).startTime < _invulnerabilityMaxLifetime)
                return;

            _pools.InvulnerabilityLifetime.Del(entity);
        }
    }
}
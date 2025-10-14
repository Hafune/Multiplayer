using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class ActionDizzySystem : AbstractActionSystem<ActionDizzyComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionDizzyComponent,
                EventDizzy,
                ActionCurrentComponent
            >,
            Exc<
                InProgressTag<ActionDeathComponent>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                ActionDizzyComponent,
                InProgressTag<ActionDizzyComponent>
            >> _progressFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var action = ref _actionPool.Value.Get(i);
                BeginActionProgress(i, action.logic);

                action.duration = _pools.EventDizzy.Get(i).duration;
                action.startTime = Time.time;
                _pools.EventDizzy.Del(i);
            }

            foreach (var i in _progressFilter.Value)
            {
                ref var action = ref _actionPool.Value.Get(i);

                if (action.duration + action.startTime < Time.time)
                    _pools.ActionComplete.AddIfNotExist(i);
            }
        }
    }
}
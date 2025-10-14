using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class ActionHitStunSystem : AbstractActionSystem<ActionHitStunComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionHitStunComponent,
                EventHitTaken,
                ActionCurrentComponent
            >,
            Exc<
                InProgressTag<ActionAttractionComponent>,
                InProgressTag<ActionDizzyComponent>,
                InProgressTag<ActionDeathComponent>
            >> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var action = ref _actionPool.Value.Get(i);

                const float ignoreHitsTime = 5;

                if (Time.time - action.lastStartTime < ignoreHitsTime)
                    continue;

                BeginActionProgress(i, action.logic);
                action.lastStartTime = Time.time;
            }
        }
    }
}
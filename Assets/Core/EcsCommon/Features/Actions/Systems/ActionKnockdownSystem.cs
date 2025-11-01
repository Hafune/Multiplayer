using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionKnockdownSystem : AbstractActionSystem<ActionKnockdownComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionKnockdownComponent,
                EventKnockdown,
                ActionCurrentComponent
            >,
            Exc<
                InProgressTag<ActionDeathComponent>
            >> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _pools.EventKnockdown.Del(i);
                var action = _actionPool.Value.Get(i);
                
                if (!action.logic.CheckConditionLogic(i))
                    continue;
                
                BeginActionProgress(i, action.logic);
            }
        }
    }
}
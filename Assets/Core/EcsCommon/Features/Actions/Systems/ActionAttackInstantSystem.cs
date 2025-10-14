using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionAttackInstantSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionAttackInstantComponent,
                EventActionStart<ActionAttackInstantComponent>
            >,
            Exc<
                InProgressTag<ActionDeathComponent>
            >> _filter;

        private readonly EcsPoolInject<ActionAttackInstantComponent> _actionPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var action = _actionPool.Value.Get(i);

                if (action.logic.CheckConditionLogic(i))
                    action.logic.StartLogic(i);
            }
        }
    }
}
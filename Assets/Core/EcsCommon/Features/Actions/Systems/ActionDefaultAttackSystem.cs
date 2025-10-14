using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionDefaultAttackSystem : AbstractActionSystem<ActionDefaultAttackComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionDefaultAttackComponent,
                EventActionStart<ActionAttackComponent>,
                ActionCurrentComponent
            >,
            Exc<
                InProgressTag<ActionDefaultAttackComponent>
            >> _activateFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                var logic = _actionPool.Value.Get(i).logic;
                if (!logic.CheckConditionLogic(i))
                    continue;

                BeginActionProgress(i, logic);
            }
            
            CleanEventStart();
        }
    }
}
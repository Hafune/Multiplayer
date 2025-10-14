using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionDashSystem : AbstractActionSystem<ActionDashComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionDashComponent,
                EventActionStart<ActionDashComponent>,
                ActionCurrentComponent
            >,
            Exc<
                InProgressTag<ActionDashComponent>
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
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class NpcActionSystem : AbstractActionSystem<NpcActionComponent>, IEcsRunSystem
    {
        protected readonly EcsFilterInject<
            Inc<
                NpcActionComponent,
                EventActionStart<NpcActionComponent>,
                ActionCurrentComponent
            >> _activateFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                _eventStartPool.Value.Del(i);                
                var logic = _actionPool.Value.Get(i).logic;
                
                if (!logic.CheckConditionLogic(i))
                    continue;

                BeginActionProgress(i, logic);
            }
        }
    }
}
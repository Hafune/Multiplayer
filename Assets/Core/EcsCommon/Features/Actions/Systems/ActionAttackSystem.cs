using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionAttackSystem : AbstractActionSystem<ActionAttackComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionAttackComponent,
                EventActionStart<ActionAttackComponent>,
                ActionCurrentComponent
            >> _startFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionAttackComponent>,
                EventActionCompleteStreaming
            >> _completeFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _startFilter.Value)
            {
                var action = _actionPool.Value.Get(i);

                if (!action.logic.CheckConditionLogic(i))
                    continue;

                BeginActionProgress(i, action.logic);
            }

            foreach (var i in _completeFilter.Value)
                _actionPool.Value.Get(i).logic?.CompleteStreamingLogic(i);
        }
    }
}
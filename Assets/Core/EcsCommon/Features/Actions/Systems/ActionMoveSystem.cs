using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class ActionMoveSystem : AbstractActionSystem<ActionMoveComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionMoveComponent,
                EventActionStart<ActionMoveComponent>,
                ActionCurrentComponent,
                MoveSpeedValueComponent,
                MoveDirectionComponent
            >,
            Exc<
                InProgressTag<ActionMoveComponent>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionMoveComponent>,
                EventActionCompleteStreaming
            >> _completeFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                var logic = _actionPool.Value.Get(i).logic;
                if (!logic.CheckConditionLogic(i))
                    continue;

                BeginActionProgress(i, logic);
                _pools.ActionCanBeCanceled.AddIfNotExist(i);
            }

            foreach (var i in _completeFilter.Value)
                _actionPool.Value.Get(i).logic?.CompleteStreamingLogic(i);

            CleanEventStart();
        }
    }
}
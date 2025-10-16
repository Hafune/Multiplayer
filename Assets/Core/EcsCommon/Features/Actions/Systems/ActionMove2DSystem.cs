using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class ActionMove2DSystem : AbstractActionSystem<ActionMoveComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionMoveComponent,
                EventActionStart<ActionMoveComponent>,
                ActionCurrentComponent,
                MoveSpeedValueComponent,
                MoveDirectionComponent,
                Rigidbody2DComponent
            >,
            Exc<
                InProgressTag<ActionMoveComponent>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionMoveComponent>,
                EventActionCompleteStreaming
            >> _completeFilter;

        public const float ActiveMass = 0.0001f;
        public const float PassiveMass = 1f;
        
        private const float _activeDrag = 0f;
        private const float _passiveDrag = 10f;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                var logic = _actionPool.Value.Get(i).logic;
                if (!logic.CheckConditionLogic(i))
                    continue;

                BeginActionProgress(i, logic);
                _pools.ActionCanBeCanceled.AddIfNotExist(i);
                var rigidbody = _pools.Rigidbody.Get(i).rigidbody; 
                rigidbody.mass = ActiveMass;
                rigidbody.linearDamping = _activeDrag;
            }

            foreach (var i in _completeFilter.Value)
                _actionPool.Value.Get(i).logic?.CompleteStreamingLogic(i);

            CleanEventStart();
        }

        public override void Cancel(int entity)
        {
            base.Cancel(entity);
            var rigidbody = _pools.Rigidbody.Get(entity).rigidbody; 
            rigidbody.mass = PassiveMass;
            rigidbody.linearDamping = _passiveDrag;
        }
    }
}
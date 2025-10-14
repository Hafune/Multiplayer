using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class ActionAttractionSystem : AbstractActionSystem<ActionAttractionComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionAttractionComponent,
                EventAttraction,
                ActionCurrentComponent,
                RigidbodyComponent
            >,
            Exc<
                InProgressTag<ActionDeathComponent>,
                InProgressTag<ActionDizzyComponent>,
                InProgressTag<ActionAttractionComponent>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                ActionAttractionComponent,
                InProgressTag<ActionAttractionComponent>,
                RigidbodyComponent
            >> _progressFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var action = ref _actionPool.Value.Get(i);
                BeginActionProgress(i, action.logic);
                var body = _pools.Rigidbody.Get(i).rigidbody;
                body.mass = ActionMoveSystem.ActiveMass;
                action.startPosition = body.position;
                action.endPosition = _pools.EventAttraction.Get(i).position;
                action.startTime = Time.time;
                _pools.EventAttraction.Del(i);
            }

            foreach (var i in _progressFilter.Value)
            {
                ref var action = ref _actionPool.Value.Get(i);
                const float destinationTime = .24f;
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = action.model.localPosition;

                if (Time.time - action.startTime > destinationTime)
                {
                    _pools.ActionComplete.AddIfNotExist(i);
                    continue;
                }

                var t = (Time.time - action.startTime) / destinationTime;
                var height = -Mathf.Sin(t * Mathf.PI);
                position.z = height * 2;
                action.model.localPosition = position;

                var velocity = (action.endPosition - action.startPosition) / destinationTime;
                body.linearVelocity = velocity;
            }
        }

        public override void Cancel(int entity)
        {
            base.Cancel(entity);
            ref var action = ref _actionPool.Value.Get(entity);
            var body = _pools.Rigidbody.Get(entity).rigidbody;
            body.linearVelocity = Vector2.zero;
            body.mass = ActionMoveSystem.PassiveMass;
            action.model.localPosition = Vector3.zero;
        }
    }
}
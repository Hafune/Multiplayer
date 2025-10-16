using System.Collections.Generic;
using Colyseus.Schema;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core
{
    public class MultiplayerUpdateClientSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated,
                RigidbodyComponent,
                Player1UniqueTag,
                MoveSpeedValueComponent
            >> _playerFilter;

        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated,
                RigidbodyComponent
            >,
            Exc<
                Player1UniqueTag
            >> _otherFilter;

        private readonly ComponentPools _pools;
        private const float SERVER_TICK_RATE = .02f;
        private const int MAX_FRAME_DELAY = 4;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _playerFilter.Value)
            {
                var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
                _pools.EventMultiplayerDataUpdated.Del(i);
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;
                var velocity = body.linearVelocity;
                var speed = velocity.magnitude * SERVER_TICK_RATE;

                UpdatePosition(ref position, ref velocity, changes);

                var distance = Vector3.Distance(position, body.position);

                if (distance > speed * MAX_FRAME_DELAY)
                    Debug.LogWarning(distance);//сделать с игроком... что то )
            }

            foreach (var i in _otherFilter.Value)
            {
                var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
                _pools.EventMultiplayerDataUpdated.Del(i);
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;
                var velocity = body.linearVelocity;
                var speed = velocity.magnitude * SERVER_TICK_RATE;

                UpdatePosition(ref position, ref velocity, changes);

                body.position = velocity == Vector3.zero || Vector3.Distance(body.position, position) > speed * MAX_FRAME_DELAY
                    ? position
                    : Vector3.Lerp(body.position, position, .5f);

                body.linearVelocity = velocity;
            }
        }

        private static void UpdatePosition(ref Vector3 position, ref Vector3 velocity, List<DataChange> changes)
        {
            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case "x":
                        position.x = (float)dataChange.Value;
                        break;
                    case "z":
                        position.z = (float)dataChange.Value;
                        break;
                    case "velocityX":
                        velocity.x = (float)dataChange.Value;
                        break;
                    case "velocityZ":
                        velocity.z = (float)dataChange.Value;
                        break;
                    default:
                        Debug.LogWarning($"Поле {dataChange.Field} не обработанно");
                        break;
                }
            }
        }
    }
}
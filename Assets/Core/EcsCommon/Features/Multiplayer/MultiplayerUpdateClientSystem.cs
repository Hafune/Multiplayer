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
                var nextPosition = body.position;
                var nextVelocity = body.linearVelocity;

                AssignValues(ref nextPosition, ref nextVelocity, changes);
                var speed = nextVelocity.magnitude * SERVER_TICK_RATE;
                var distance = Vector3.Distance(nextPosition, body.position);

                if (distance > speed * MAX_FRAME_DELAY)
                    Debug.LogWarning(distance); //сделать с игроком... что то )
                //этот блок пока не корректен т.к. я ещё не понимаю что делать игроку с его данными пришедшими с сервера )
            }

            foreach (var i in _otherFilter.Value)
            {
                var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
                _pools.EventMultiplayerDataUpdated.Del(i);
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var nextPosition = body.position;
                var nextVelocity = body.linearVelocity;

                AssignValues(ref nextPosition, ref nextVelocity, changes);
                var speed = nextVelocity.magnitude * SERVER_TICK_RATE;

                var currentPosition = body.position;
                body.position = Vector3.Distance(currentPosition, nextPosition) > speed * MAX_FRAME_DELAY
                    ? nextPosition
                    : Vector3.Lerp(currentPosition, nextPosition, .5f);

                body.linearVelocity = nextVelocity;
            }
        }

        private static void AssignValues(ref Vector3 position, ref Vector3 velocity, List<DataChange> changes)
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
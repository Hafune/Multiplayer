using System.Collections.Generic;
using Colyseus.Schema;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core
{
    public class MultiplayerUpdateClientSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated,
                MultiplayerPositionComponent,
                AnimatorComponent,
                PositionComponent
            >,
            Exc<
                Player1UniqueTag
            >> _otherFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<MultiplayerPositionComponent>,
                MultiplayerPositionComponent,
                PositionComponent
            >> _positionFilter;

        private readonly ComponentPools _pools;
        private readonly MySerializablePose _pose = new();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _otherFilter.Value)
            {
                var update = _pools.EventMultiplayerDataUpdated.Get(i);
                _pools.EventMultiplayerDataUpdated.Del(i);
                var transform = _pools.Position.Get(i).transform;

                ref var target = ref _pools.MultiplayerPosition.Get(i);
                var position = target.position;
                var velocity = target.velocity;
                var euler = transform.eulerAngles;
                var bodyAngle = euler.y;

                AssignValues(ref position, ref velocity, ref bodyAngle, out var state, update.changes);

                euler.y = bodyAngle;

                target.rotation = Quaternion.Euler(euler);
                target.position = position;
                target.prediction = position + velocity * update.delay;
                target.distance = (target.position - transform.position).magnitude;
                target.velocity = velocity;
                target.delay = update.delay;

                _pools.InProgressMultiplayerPosition.AddIfNotExist(i);

                if (string.IsNullOrEmpty(state))
                    continue;

                var animancer = _pools.Animator.Get(i).animancer;
                JsonUtility.FromJsonOverwrite(state, _pose);
                _pose.ApplyTo(animancer);
            }

            foreach (var i in _positionFilter.Value)
            {
                var target = _pools.MultiplayerPosition.Get(i);
                var transform = _pools.Position.Get(i).transform;
                transform.position =
                    Vector3.MoveTowards(transform.position, target.prediction, Time.deltaTime / target.delay * target.distance);

                var angularSpeed = Quaternion.Angle(transform.rotation, target.rotation) / target.delay;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, Time.deltaTime * angularSpeed);

                if (transform.position == target.position && transform.rotation == target.rotation)
                    _pools.InProgressMultiplayerPosition.Del(i);
            }
        }

        private static void AssignValues(
            ref Vector3 position,
            ref Vector3 velocity,
            ref float bodyAngle,
            out string states,
            List<DataChange> changes)
        {
            states = "";
            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case nameof(Player.x):
                        position.x = (float)dataChange.Value;
                        break;
                    case nameof(Player.y):
                        position.y = (float)dataChange.Value;
                        break;
                    case nameof(Player.z):
                        position.z = (float)dataChange.Value;
                        break;
                    case nameof(Player.velocityX):
                        velocity.x = (float)dataChange.Value;
                        break;
                    case nameof(Player.velocityY):
                        velocity.y = (float)dataChange.Value;
                        break;
                    case nameof(Player.velocityZ):
                        velocity.z = (float)dataChange.Value;
                        break;
                    case nameof(Player.bodyAngle):
                        bodyAngle = (float)dataChange.Value;
                        break;
                    case nameof(Player.state):
                        states = (string)dataChange.Value;
                        break;
                    default:
                        Debug.LogWarning($"Поле {dataChange.Field} не обработанно");
                        break;
                }
            }
        }
    }
}
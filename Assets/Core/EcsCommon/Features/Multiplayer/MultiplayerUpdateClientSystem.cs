using System.Collections.Generic;
using Colyseus.Schema;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine;

namespace Core
{
    public class MultiplayerUpdateClientSystem : IEcsRunSystem
    {
        // private readonly EcsFilterInject<
        //     Inc<
        //         EventMultiplayerDataUpdated,
        //         AnimatorComponent,
        //         RigidbodyComponent,
        //         Player1UniqueTag,
        //         MoveSpeedValueComponent
        //     >> _playerFilter;

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
        private readonly AnimationClip[] _clips;
        private readonly List<int> _states = new();
        private readonly MySerializablePose _pose = new();

        public MultiplayerUpdateClientSystem(Context context) => _clips = context.Resolve<MultiplayerManager>().GetStates();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _otherFilter.Value)
            {
                var update = _pools.EventMultiplayerDataUpdated.Get(i);
                _pools.EventMultiplayerDataUpdated.Del(i);
                var transform = _pools.Position.Get(i).transform;

                ref var target = ref _pools.MultiplayerPosition.Get(i);
                var position = target.position;
                var velocity = Vector3.zero;
                var euler = transform.eulerAngles;
                var bodyAngle = euler.y;

                bool stateWasChanged = false;
                AssignValues(ref position, ref velocity, ref bodyAngle, out var state, ref stateWasChanged, update.changes);

                euler.y = bodyAngle;

                target.rotation = Quaternion.Euler(euler);
                target.position = position + velocity * update.delay;
                target.speed = (target.position - transform.position).magnitude;
                target.delay = update.delay;
                _pools.InProgressMultiplayerPosition.AddIfNotExist(i);
                
                if (string.IsNullOrEmpty(state))
                    continue;

                var animancer = _pools.Animator.Get(i).animancer;
                
                JsonUtility.FromJsonOverwrite(state, _pose);

                _pose.ApplyTo(animancer);
                
                // var layers = animancer.Layers;
                // int index = 0;
                // int lIndex = 0;
                //
                // for (; index < _states.Count; index += 2)
                // {
                //     var id = _states[index];
                //     var layer = layers[lIndex];
                //     layer.Play(_clips[id]);
                //     layer.Weight = _states[index + 1] / MultiplayerUpdateServerSystem.LAYER_WEIGHT_SCALE;
                //     lIndex++;
                // }
                //
                // for (; lIndex < layers.Count; lIndex++)
                //     layers[lIndex].Stop();
            }

            foreach (var i in _positionFilter.Value)
            {
                var target = _pools.MultiplayerPosition.Get(i);
                var transform = _pools.Position.Get(i).transform;
                var position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime / target.delay * target.speed);
                transform.position = position;

                var angularSpeed = Quaternion.Angle(transform.rotation, target.rotation) / target.delay;
                var maxDelta = Time.deltaTime * angularSpeed;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, maxDelta);

                if (transform.position == target.position && transform.rotation == target.rotation)
                    _pools.InProgressMultiplayerPosition.Del(i);
            }
        }

        private static void AssignValues(
            ref Vector3 position,
            ref Vector3 velocity,
            ref float bodyAngle,
            out string states,
            ref bool stateWasChanged,
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
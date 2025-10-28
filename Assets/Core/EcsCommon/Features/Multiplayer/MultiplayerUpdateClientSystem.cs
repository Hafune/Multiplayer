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
                AnimatorComponent,
                PositionComponent,
                ViewAnimationsComponent
            >,
            Exc<
                Player1UniqueTag
            >> _readPositionFilter;

        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated,
                HitPointValueComponent,
                HitPointMaxValueComponent
            >> _readHitPointFilter;

        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated
            >> _eventFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<MultiplayerDataComponent>,
                MultiplayerDataComponent,
                PositionComponent
            >> _movePositionFilter;

        private readonly ComponentPools _pools;
        private readonly MySerializablePose _pose = new();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _readPositionFilter.Value)
            {
                var update = _pools.EventMultiplayerDataUpdated.Get(i);
                var transform = _pools.Position.Get(i).transform;

                ref var target = ref _pools.MultiplayerData.Get(i);
                var euler = transform.eulerAngles;
                var bodyAngle = euler.y;

                bool wasIdle = target.velocity == Vector3.zero && target.delay != 0;
                bool wasRun = target.velocity != Vector3.zero && target.delay != 0;

                AssignValues(ref target.position, ref target.velocity, ref bodyAngle, out var state, update.changes);

                euler.y = bodyAngle;

                target.rotation = Quaternion.Euler(euler);
                target.prediction = target.position + target.velocity * update.delay;
                target.distance = (target.position - transform.position).magnitude;
                target.delay = update.delay;
                _pools.InProgressMultiplayerData.AddIfNotExist(i);

                var animations = _pools.ViewAnimations.Get(i);

                var dir = Quaternion.Inverse(target.rotation) * target.velocity;

                if (target.velocity == Vector3.zero)
                {
                    if (!wasIdle)
                        animations.idle.Run(i);
                }
                else
                {
                    if (!wasRun)
                        animations.move.Run(i);

                    dir.Normalize();
                    animations.moveDirection.SmoothedParameter.TargetValue = new(dir.x, dir.z);
                }

                if (string.IsNullOrEmpty(state))
                    continue;

                var animancer = _pools.Animator.Get(i).animancer;
                JsonUtility.FromJsonOverwrite(state, _pose);
                _pose.ApplyTo(animancer);
            }

            foreach (var i in _readHitPointFilter.Value)
            {
                var update = _pools.EventMultiplayerDataUpdated.Get(i);

                ref var hitPoint = ref _pools.HitPointValue.Get(i);
                ref var hitPointMax = ref _pools.HitPointMaxValue.Get(i);
                int hp = (int)hitPoint.value;
                int hpMax = (int)hitPointMax.value;

                AssignHitPointValues(ref hp, ref hpMax, update.changes);

                if ((int)hitPoint.value != hp)
                {
                    hitPoint.value = hp;
                    _pools.EventUpdatedHitPointValue.AddIfNotExist(i);
                    
                    if (hitPoint.value <= 0) 
                        _pools.EventStartActionDeath.AddIfNotExist(i);
                }
                
                if ((int)hitPointMax.value != hp)
                {
                    hitPointMax.value = hpMax;
                    _pools.EventUpdatedHitPointMaxValue.AddIfNotExist(i);
                }
            }

            foreach (var i in _eventFilter.Value)
                _pools.EventMultiplayerDataUpdated.Del(i);

            foreach (var i in _movePositionFilter.Value)
            {
                var target = _pools.MultiplayerData.Get(i);
                var transform = _pools.Position.Get(i).transform;

                if (transform.position == target.prediction && transform.rotation == target.rotation)
                {
                    _pools.InProgressMultiplayerData.Del(i);
                    continue;
                }

                if (target.distance > .5f)
                {
                    transform.position =
                        Vector3.MoveTowards(transform.position, target.prediction, Time.deltaTime / target.delay * target.distance);
                }
                else if (target.velocity != Vector3.zero)
                {
                    transform.position =
                        Vector3.MoveTowards(transform.position, target.prediction, target.velocity.magnitude * Time.deltaTime);
                }
                else
                {
                    transform.position = target.position;
                }

                var angularSpeed = Quaternion.Angle(transform.rotation, target.rotation) / target.delay;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, Time.deltaTime * angularSpeed);
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
                }
            }
        }

        private static void AssignHitPointValues(
            ref int currentHp,
            ref int maxHp,
            List<DataChange> changes)
        {
            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case nameof(Player.currentHp):
                        currentHp = (short)dataChange.Value;
                        break;
                    case nameof(Player.maxHp):
                        maxHp = (short)dataChange.Value;
                        break;
                }
            }
        }
    }
}
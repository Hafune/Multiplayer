using System.Collections.Generic;
using Colyseus.Schema;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core
{
    public class MultiplayerUpdateClientSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated,
                AnimatorComponent,
                RigidbodyComponent,
                Player1UniqueTag,
                MoveSpeedValueComponent
            >> _playerFilter;

        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated,
                AnimatorComponent,
                RigidbodyComponent
            >,
            Exc<
                Player1UniqueTag
            >> _otherFilter;

        private readonly ComponentPools _pools;
        private readonly Dictionary<AnimationClip, int> _stateIds;
        private readonly AnimationClip[] _clips;
        private readonly List<int> _states = new();
        private const float SERVER_TICK_RATE = .02f;
        private const int MAX_FRAME_DELAY = 4;

        public MultiplayerUpdateClientSystem(Context context) => (_stateIds, _clips) = context.Resolve<MultiplayerManager>().GetStates();

        public void Run(IEcsSystems systems)
        {
            // foreach (var i in _playerFilter.Value)
            // {
            //     var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
            //     _pools.EventMultiplayerDataUpdated.Del(i);
            //     var body = _pools.Rigidbody.Get(i).rigidbody;
            //     var nextPosition = body.position;
            //     var nextVelocity = body.linearVelocity;
            //
            //     AssignValues(ref nextPosition, ref nextVelocity, changes);
            //     var speed = nextVelocity.magnitude * SERVER_TICK_RATE;
            //     var distance = Vector3.Distance(nextPosition, body.position);
            //
            //     if (distance > speed * MAX_FRAME_DELAY)
            //         Debug.LogWarning(distance); //сделать с игроком... что то )
            //     //этот блок пока не корректен т.к. я ещё не понимаю что делать игроку с его данными пришедшими с сервера )
            // }

            foreach (var i in _otherFilter.Value)
            {
                var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
                _pools.EventMultiplayerDataUpdated.Del(i);
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var nextPosition = body.position;
                var nextVelocity = body.linearVelocity;
                var euler = body.transform.eulerAngles;
                var bodyAngle = euler.y;

                bool stateWasChanged = false;
                AssignValues(ref nextPosition, ref nextVelocity, ref bodyAngle, _states, ref stateWasChanged, changes);
                var speed = nextVelocity.magnitude * SERVER_TICK_RATE;

                euler.y = bodyAngle;
                body.transform.eulerAngles = euler;

                var currentPosition = body.position;
                body.position = Vector3.Distance(currentPosition, nextPosition) > speed * MAX_FRAME_DELAY
                    ? nextPosition
                    : Vector3.Lerp(currentPosition, nextPosition, .5f);

                body.linearVelocity = nextVelocity;

                if (!stateWasChanged)
                    continue;

                int index = 0;
                var layers = _pools.Animator.Get(i).animancer.Layers;
                foreach (var id in _states) 
                    layers[index++].Play(_clips[id]);

                index++;
                for (; index < layers.Count; index++)
                    layers[index].Stop();
            }
        }

        private static void AssignValues(
            ref Vector3 position,
            ref Vector3 velocity,
            ref float bodyAngle,
            List<int> states,
            ref bool stateWasChanged,
            List<DataChange> changes)
        {
            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case nameof(Player.x):
                        position.x = (float)dataChange.Value;
                        break;
                    case nameof(Player.y):
                        position.z = (float)dataChange.Value;
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
                        states.Clear();
                        ((ArraySchema<int>)dataChange.Value).ForEach(states.Add);
                        stateWasChanged = true;
                        break;
                    default:
                        Debug.LogWarning($"Поле {dataChange.Field} не обработанно");
                        break;
                }
            }
        }
    }
}
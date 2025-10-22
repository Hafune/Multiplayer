using System.Collections.Generic;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core
{
    public class MultiplayerUpdateServerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                Player1UniqueTag,
                AnimatorComponent,
                RigidbodyComponent
            >> _moveFilter;

        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerEntityCreated,
                RigidbodyComponent
            >> _spawnFilter;

        private readonly ComponentPools _pools;
        private readonly Dictionary<string, object> _message = new();
        private readonly List<AnimationClip> _clips = new();
        private readonly List<float> _weights = new();
        private readonly Dictionary<AnimationClip, int> _stateIds;
        private readonly List<int> _states = new();

        public MultiplayerUpdateServerSystem(Context context) => (_stateIds, _) = context.Resolve<MultiplayerManager>().GetStates();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _moveFilter.Value)
            {
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var bodyAngle = body.transform.eulerAngles.y;
                _clips.Clear();
                foreach (var layer in _pools.Animator.Get(i).animancer.Layers)
                {
                    if (layer.CurrentState.IsPlaying)
                    {
                        layer.CurrentState.GatherAnimationClips(_clips);
                        _weights.Add(layer.CurrentState.Weight);
                    }
                }

                _states.Clear();
                foreach (var clip in _clips)
                    _states.Add(_stateIds[clip]);

                var position = body.position;
                var velocity = body.linearVelocity;
                _message.Clear();
                _message["x"] = position.x;
                _message["z"] = position.z;
                _message["velocityX"] = velocity.x;
                _message["velocityZ"] = velocity.z;
                _message["bodyAngle"] = bodyAngle;
                _message["state"] = _states;

                MultiplayerManager.Instance.SendData("move", _message);
            }

            foreach (var i in _spawnFilter.Value)
            {
                _pools.EventMultiplayerEntityCreated.Del(i);
                var templateId = _pools.ConvertToEntity.Get(i).convertToEntity.TemplateId;
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;
                var velocity = body.linearVelocity;

                var info = new SpawnInfo
                {
                    x = position.x,
                    y = position.y,
                    z = position.z,
                    velocityX = velocity.x,
                    velocityY = velocity.y,
                    velocityZ = velocity.z,
                    templateId = templateId,
                    ownerClientId = MultiplayerManager.Instance.GetClientId()
                };

                MultiplayerManager.Instance.SendMessage("shoot", JsonUtility.ToJson(info));
            }
        }
    }
}
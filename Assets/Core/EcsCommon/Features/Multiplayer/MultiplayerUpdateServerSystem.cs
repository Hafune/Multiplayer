using System.Collections.Generic;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core
{
    public class MultiplayerUpdateServerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                Player1UniqueTag,
                AnimatorComponent,
                RigidbodyComponent,
                MultiplayerStateComponent
            >> _updateServerFilter;

        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerEntityCreated,
                RigidbodyComponent
            >> _spawnFilter;

        private readonly EcsFilterInject<
            Inc<
                EventCausedDamage
            >,
            Exc<
                InvulnerabilityLifetimeComponent
            >> _damageFilter;

        private readonly ComponentPools _pools;

        private readonly Dictionary<string, object> _message = new();
        private readonly List<int> _damages = new();

        private readonly List<string> _targets = new();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _updateServerFilter.Value)
            {
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var bodyAngle = body.transform.eulerAngles.y;

                var position = body.position;
                var velocity = body.linearVelocity;
                _message.Clear();
                _message["x"] = position.x;
                _message["y"] = position.y;
                _message["z"] = position.z;
                _message["velocityX"] = velocity.x;
                _message["velocityY"] = velocity.y;
                _message["velocityZ"] = velocity.z;
                _message["bodyAngle"] = bodyAngle;
                _message["state"] = _pools.MultiplayerState.Get(i).state.GetState();

                MultiplayerManager.Instance.SendData("move", _message);
            }

            foreach (var i in _spawnFilter.Value)
            {
                _pools.EventMultiplayerEntityCreated.Del(i);
                var templateId = _pools.ConvertToEntity.Get(i).convertToEntity.TemplateId;
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;
                var velocity = body.linearVelocity;

                var info = new MultiplayerSpawnInfo
                {
                    x = position.x,
                    y = position.y,
                    z = position.z,
                    velocityX = velocity.x,
                    velocityY = velocity.y,
                    velocityZ = velocity.z,
                    templateId = templateId,
                    key = MultiplayerManager.Instance.GetClientId()
                };

                MultiplayerManager.Instance.SendData("shoot", JsonUtility.ToJson(info));
            }

            foreach (var i in _damageFilter.Value)
            {
                _message.Clear();
                _targets.Clear();
                _damages.Clear();
                
                foreach (var (target, damage) in _pools.EventCausedDamage.Get(i))
                {
                    var id = _pools.MultiplayerData.Get(target).data.SessionId;
                    _targets.Add(id);
                    _damages.Add((int)damage);
                }

                _pools.EventCausedDamage.Del(i);
                
                _message["ids"] = _targets;
                _message["damages"] = _damages;

                MultiplayerManager.Instance.SendData("damage", _message);
            }
        }
    }
}
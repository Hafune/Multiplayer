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
        private const float _serverFps = 1 / 20f;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _playerFilter.Value)
            {
                var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
                _pools.EventMultiplayerDataUpdated.Del(i);
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;

                UpdatePosition(ref position, changes);

                var distance = Vector3.Distance(position, body.position);
                var speed = _pools.MoveSpeedValue.Get(i).value;

                if (distance > speed * _serverFps)
                    Debug.LogWarning(distance);
            }

            foreach (var i in _otherFilter.Value)
            {
                var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
                _pools.EventMultiplayerDataUpdated.Del(i);
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;

                UpdatePosition(ref position, changes);
                
                body.MovePosition(position);
            }
        }

        private void UpdatePosition(ref Vector3 position, List<DataChange> changes)
        {
            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case "x":
                        SetOffset(ref position.x, dataChange);
                        break;
                    case "z":
                        SetOffset(ref position.z, dataChange);
                        break;
                    default:
                        Debug.LogWarning($"Поле {dataChange.Field} не обработанно");
                        break;
                }
            }
        }

        private void SetOffset(ref float value, DataChange dataChange)
        {
            var current = (float)dataChange.Value;
            var previous = (float)dataChange.PreviousValue;
            var extrude = current - previous;
            value = current + extrude;
            
            if (extrude == 0)
                Debug.Log("0");
        }
    }
}
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core
{
    public class MultiplayerUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventMultiplayerDataUpdated,
                PositionComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var changes = _pools.EventMultiplayerDataUpdated.Get(i).changes;
                _pools.EventMultiplayerDataUpdated.Del(i);
                var transform = _pools.Position.Get(i).transform;
                var position = transform.position;

                foreach (var dataChange in changes)
                {
                    switch (dataChange.Field)
                    {
                        case "x":
                            position.x = (float)dataChange.Value;
                            break;
                        case "y":
                            position.z = (float)dataChange.Value;
                            break;
                        default:
                            Debug.LogWarning($"Поле {dataChange.Field} не обработанно");
                            break;
                    }
                }

                transform.position = position;
            }
        }
    }
}
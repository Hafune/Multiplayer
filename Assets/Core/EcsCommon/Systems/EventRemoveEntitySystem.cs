using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventRemoveEntitySystem : IEcsRunSystem
    {
        private EcsWorldInject _world;

        private readonly EcsFilterInject<
            Inc<
                ConvertToEntityComponent,
                EventRemoveEntity
            >> _convertToEntityFilter;

        private readonly EcsFilterInject<
            Inc<
                EventRemoveEntity
            >> _eventFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _convertToEntityFilter.Value)
                _pools.ConvertToEntity.Get(i).convertToEntity.RemoveConnectionInfo();
            
            foreach (var i in _eventFilter.Value)
                _world.Value.DelEntity(i);
        }
    }
}
//Файл генерируется в GenRemoveEventUpdatedSlotTagSystem
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{
    // @formatter:off
    public class RemoveEventUpdatedSlotTagSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventValueUpdated<ThroughProjectileSlotTag>>> _eventUpdatedThroughProjectileSlotTagFilter;
                
        private readonly EcsPoolInject<EventValueUpdated<ThroughProjectileSlotTag>> _eventUpdatedThroughProjectileSlotTagPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _eventUpdatedThroughProjectileSlotTagFilter.Value) _eventUpdatedThroughProjectileSlotTagPool.Value.Del(i);
        }
    }
}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class InitRecalculateAllValuesSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventInit>> _hasInitFilter;

        private readonly EcsPoolInject<EventStartRecalculateAllValues> _pool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _hasInitFilter.Value)
                _pool.Value.AddIfNotExist(i);
        }
    }
}
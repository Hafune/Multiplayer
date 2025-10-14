using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class SumValueByPercentSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventValueUpdated<V>, 
                M, 
                V
            >> _filter;

        private readonly EcsPoolInject<V> _pool;
        private readonly EcsPoolInject<M> _byPool;
        private readonly float _scale;

        public SumValueByPercentSystem(float scale = 1f) => _scale = scale;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pool.Value.Get(i).value += _pool.Value.Get(i).value * _byPool.Value.Get(i).value * _scale;
        }
    }
}
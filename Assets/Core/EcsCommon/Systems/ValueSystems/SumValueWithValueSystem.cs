using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class SumValueByValueSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventValueUpdated<V>, M, V
            >> _filter;

        private readonly EcsPoolInject<V> _pool;
        private readonly EcsPoolInject<M> _byPool;
        private readonly float _scale;

        public SumValueByValueSystem(float scale = 1) => _scale = scale;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pool.Value.Get(i).value += _byPool.Value.Get(i).value * _scale;
        }
    }
}
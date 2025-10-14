using System;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ScaleValueSystem<T> : IEcsRunSystem
        where T : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                T,
                EventValueUpdated<T>,
                ConvertToEntityComponent,
                EnemyComponent
            >> _filter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<ConvertToEntityComponent> _convertPool;

        private readonly Func<int, float> _getScale;

        public ScaleValueSystem(Func<int, float> getScale) => _getScale = getScale;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var value = _valuePool.Value.Get(i).value;
                var value2 = _getScale.Invoke(_convertPool.Value.Get(i).convertToEntity.TemplateId);
                _valuePool.Value.Get(i).value = value * value2;
            }
        }
    }
}
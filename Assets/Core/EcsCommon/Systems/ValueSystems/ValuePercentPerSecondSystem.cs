using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Systems
{
    public class ValuePercentPerSecondSystem<V, M, P> : IEcsRunSystem
        where V : struct, IValue //текущее значение
        where M : struct, IValue //максимальное
        where P : struct, IValue //per second value
    {
        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                EventValueUpdated<V>
            >> _clampFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                EventValueUpdated<M>
            >> _clampByMaxFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                P,
                EventValueUpdated<V>
            >,
            Exc<
                InProgressTag<ValuePerSecondSystem<V, M, P>>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                P,
                EventValueUpdated<M>
            >,
            Exc<
                InProgressTag<ValuePerSecondSystem<V, M, P>>
            >> _activateByMaxFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                P,
                EventValueUpdated<P>
            >,
            Exc<
                InProgressTag<ValuePerSecondSystem<V, M, P>>
            >> _activateByPerSecondFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                P,
                InProgressTag<ValuePerSecondSystem<V, M, P>>
            >,
            Exc<
                InProgressTag<ActionDeathComponent>,
                EventActionStart<ActionDeathComponent>
            >> _progressFilter;

        private readonly EcsPoolInject<V> _valuePool;
        private readonly EcsPoolInject<M> _maxValuePool;
        private readonly EcsPoolInject<P> _percentPerSecondValuePool;
        private readonly EcsPoolInject<InProgressTag<ValuePerSecondSystem<V, M, P>>> _progressPool;
        private readonly EcsPoolInject<EventValueUpdated<V>> _eventValueUpdatedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _clampFilter.Value)
                _valuePool.Value.Get(i).value =
                    math.clamp(_valuePool.Value.Get(i).value, 0, _maxValuePool.Value.Get(i).value);

            foreach (var i in _clampByMaxFilter.Value)
                _valuePool.Value.Get(i).value =
                    math.clamp(_valuePool.Value.Get(i).value, 0, _maxValuePool.Value.Get(i).value);

            foreach (var i in _activateFilter.Value)
                Activate(i);

            foreach (var i in _activateByMaxFilter.Value)
                Activate(i);

            foreach (var i in _activateByPerSecondFilter.Value)
                Activate(i);

            foreach (var i in _progressFilter.Value)
                UpdateEntity(i);
        }

        private void Activate(int entity)
        {
            if (_percentPerSecondValuePool.Value.Get(entity).value == 0)
                return;

            _progressPool.Value.Add(entity);
        }

        private void UpdateEntity(int entity)
        {
            var percent = _percentPerSecondValuePool.Value.Get(entity).value;

            if (percent != 0)
            {
                ref var value = ref _valuePool.Value.Get(entity);
                var maxValue = _maxValuePool.Value.Get(entity).value;
                value.value += maxValue * percent * Time.deltaTime;
                _eventValueUpdatedPool.Value.AddIfNotExist(entity);

                if (value.value < maxValue && value.value > 0)
                    return;

                value.value = math.clamp(value.value, 0, maxValue);
                _progressPool.Value.Del(entity);
            }
            else
            {
                _progressPool.Value.Del(entity);
            }
        }
    }
}
using System;
using Core.Components;
using Core.Lib;
using Leopotam.EcsLite;
using Lib;
using Reflex;

namespace Core
{
    public readonly struct ManualValueListener<T> where T : struct
    {
        private readonly ConvertToEntity _convertToEntity;
        private readonly EcsPool<T> _pool;
        private readonly EcsPool<LocalUiValue<T>> _uiPool;
        private readonly Action<T> _valueUpdated;

        public ManualValueListener(ConvertToEntity convertToEntity, Context context, Action<T> valueUpdated)
        {
            _convertToEntity = convertToEntity;
            var world = context.Resolve<EcsWorld>();
            _pool = world.GetPool<T>();
            _uiPool = world.GetPool<LocalUiValue<T>>();
            _valueUpdated = valueUpdated;
        }

        public T Get() => _pool.Get(_convertToEntity.RawEntity);

        public void Enable() => _uiPool.GetOrInitialize(_convertToEntity.RawEntity).update += _valueUpdated;

        public void Disable() => _uiPool.GetOrInitialize(_convertToEntity.RawEntity).update -= _valueUpdated;
    }
}
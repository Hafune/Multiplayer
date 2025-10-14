using System;
using Core.Components;
using Core.Lib;
using Leopotam.EcsLite;
using Lib;

namespace Core
{
    public readonly struct ValueListener<T> where T : struct
    {
        private readonly ConvertToEntity _convertToEntity;
        private readonly EcsPool<T> _pool;

        public ValueListener(ConvertToEntity convertToEntity, Action<T> valueUpdated)
        {
            _convertToEntity = convertToEntity;
            var world = convertToEntity.GetContext().Resolve<EcsWorld>();
            _pool = world.GetPool<T>();
            var uiPool = world.GetPool<LocalUiValue<T>>();
            //лямбда в куче допустима для этого случая 
            _convertToEntity.RegisterInitializeCall(() => uiPool.GetOrInitialize(convertToEntity.RawEntity).update += valueUpdated);
        }

        public T Get() => _pool.Get(_convertToEntity.RawEntity);
    }
}
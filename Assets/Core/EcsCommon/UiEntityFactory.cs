using System;
using System.Collections.Generic;
using Core.Components;
using Core.Lib;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core
{
    public class UiEntityFactory
    {
        private readonly EcsWorld _world;
        private readonly Dictionary<Type, HashSet<Type>> _existingSystemTypes = new();
        private readonly Dictionary<Type, HashSet<Type>> _existingEventSystemTypes = new();
        private readonly EcsEngine _ecsEngine;

        public UiEntityFactory(EcsWorld world, EcsEngine ecsEngine)
        {
            _world = world;
            _ecsEngine = ecsEngine;
        }

        public IUiEntityBuilder BuildUiEntityWithLink<T>()
            where T : struct
        {
            var entity = _world.NewEntity();
            _world.GetPool<GlobalUiLink<T>>().Add(entity);

            return new UiEntityBuilder<T>(
                entity,
                _existingSystemTypes,
                _existingEventSystemTypes,
                _ecsEngine,
                _world);
        }

        private class UiEntityBuilder<T> : IUiEntityBuilder where T : struct
        {
            private readonly Dictionary<Type, HashSet<Type>> _existingSystemTypes;
            private readonly Dictionary<Type, HashSet<Type>> _existingEventSystemTypes;
            private readonly EcsEngine _ecsEngine;
            private readonly EcsWorld _world;

            public int UiRawEntity { get; }

            public UiEntityBuilder(
                int entity,
                Dictionary<Type, HashSet<Type>> existingSystemTypes,
                Dictionary<Type, HashSet<Type>> existingEventSystemTypes,
                EcsEngine ecsEngine,
                EcsWorld world
            )
            {
                UiRawEntity = entity;
                _existingSystemTypes = existingSystemTypes;
                _existingEventSystemTypes = existingEventSystemTypes;
                _ecsEngine = ecsEngine;
                _world = world;
            }

            public IUiEntityBuilder ValueUpdated<T1>(Action<int, T1> refreshFunction) where T1 : struct
            {
                _world.GetPool<UiValue<T1>>().Add(UiRawEntity).update = refreshFunction;

                var type0 = typeof(T);
                var type1 = typeof(T1);

                if (!_existingSystemTypes.ContainsKey(type0))
                    _existingSystemTypes.Add(type0, new HashSet<Type>());

                if (_existingSystemTypes[type0].Contains(type1))
                    return this;

                _existingSystemTypes[type0].Add(type1);
                _ecsEngine.AddUiSystem(new UpdateGlobalUiSystem<T, T1>());

                return this;
            }

            public IUiEntityBuilder OnEvent<T1>(Action<int, T1> refreshFunction) where T1 : struct
            {
                _world.GetPool<UiValue<T1>>().Add(UiRawEntity).update = refreshFunction;

                var type0 = typeof(T);
                var type1 = typeof(T1);

                if (!_existingEventSystemTypes.ContainsKey(type0))
                    _existingEventSystemTypes.Add(type0, new HashSet<Type>());

                if (_existingEventSystemTypes[type0].Contains(type1))
                    return this;

                _existingEventSystemTypes[type0].Add(type1);

                if (type1 == typeof(EventRemoveEntity))
                    _ecsEngine.AddRemoveEntityReactionUiSystem(new UpdateGlobalUiByEventSystem<T, T1>());
                else
                    _ecsEngine.AddUiSystem(new UpdateGlobalUiByEventSystem<T, T1>());

                return this;
            }
        }
    }
}
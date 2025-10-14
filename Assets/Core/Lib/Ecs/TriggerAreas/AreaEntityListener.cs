using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Leopotam.EcsLite;
using Lib;

namespace Core.Lib
{
    public class AreaEntityListener
    {
        public readonly HashSet<ConvertToEntity> entities = new();

        private ConvertToEntity _entityRef;
        private IEcsPool _pool;
        private readonly Action<ConvertToEntity> _del;

        public AreaEntityListener() => _del = Del;

        public void SetPools<T>(ConvertToEntity entityRef, EcsWorld world) where T : struct
        {
            _entityRef = entityRef;
            _pool = world.GetPool<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddComponent()
        {
            if (_entityRef?.RawEntity > -1)
                _pool.AddIfNotExist(_entityRef.RawEntity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RemoveComponent()
        {
            if (_entityRef?.RawEntity > -1)
                _pool.DelIfExist(_entityRef.RawEntity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(ConvertToEntity entityRef)
        {
            if (!entities.Add(entityRef))
                return;

            entityRef.BeforeEntityDeleted += _del;
            AddComponent();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEntities(AreaEntityTriggersListener listener)
        {
            foreach (var entityRef in listener.entities.Keys)
                Add(entityRef);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Del(ConvertToEntity entityRef)
        {
            if (!entities.Remove(entityRef))
                return;

            entityRef.BeforeEntityDeleted -= _del;
            entities.Remove(entityRef);

            if (entities.Count == 0)
                RemoveComponent();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            foreach (var entityRef in entities)
                entityRef.BeforeEntityDeleted -= _del;

            entities.Clear();
            RemoveComponent();
        }
    }
}
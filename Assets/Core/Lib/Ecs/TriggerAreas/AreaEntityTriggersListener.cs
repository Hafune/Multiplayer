using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class AreaEntityTriggersListener
    {
        private static readonly MyList<MyList<Component>> SetPool = new();
        public readonly Dictionary<ConvertToEntity, MyList<Component>> entities = new();

        private IEcsPool _pool;
        [CanBeNull] private ConvertToEntity _entityRef;
        private readonly Action<ConvertToEntity> _deleteCompletely;

        public AreaEntityTriggersListener() => _deleteCompletely = DeleteCompletely;

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
        public void Add(ConvertToEntity entityRef, Component col)
        {
            if (entities.TryGetValue(entityRef, out var set))
            {
                set.Add(col);
                return;
            }

            entities[entityRef] = set = SetPool.Count > 0 ? SetPool.Pop() : new();
            set.Add(col);
            entityRef.BeforeEntityDeleted += _deleteCompletely;
            AddComponent();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEntities(AreaEntityTriggersListener listener)
        {
            foreach (var (entityRef, externalSet) in listener.entities)
            foreach (var col in externalSet)
                Add(entityRef, col);
        }

        private void DeleteCompletely(ConvertToEntity entityRef)
        {
            var set = entities[entityRef];
            set.Clear();
            SetPool.Add(set);
            entities.Remove(entityRef);

            entityRef.BeforeEntityDeleted -= _deleteCompletely;

            if (entities.Count == 0)
                RemoveComponent();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Del(ConvertToEntity entityRef, Component col)
        {
            if (!entities.TryGetValue(entityRef, out var set))
                return false;

            set.Remove(col);
            if (set.Count != 0)
                return false;

            entityRef.BeforeEntityDeleted -= _deleteCompletely;
            entities.Remove(entityRef);
            SetPool.Add(set);

            if (entities.Count == 0)
                RemoveComponent();

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            foreach (var entityRef in entities.Keys)
            {
                var set = entities[entityRef];
                set.Clear();
                SetPool.Add(set);
                entityRef.BeforeEntityDeleted -= _deleteCompletely;
            }

            entities.Clear();
            RemoveComponent();
        }
    }
}
using System;
using System.Linq;
using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class AreaBase
    {
        private readonly ConvertToEntity _entityRef;
        private readonly bool _autoClearHitOnExit;
        private readonly AreaEntityListener _processed = new();
        private readonly AreaEntityListener _leavers = new();
        private readonly AreaEntityTriggersListener _potentialLeavers = new();
        private readonly AreaEntityTriggersListener _contacts = new();
        private readonly AreaEntityTriggersListener _receivers = new();
        private bool _isInitialized;

        public Vector2 TriggerPoint { get; private set; }
        public int ReceiversClearCount { get; private set; }

        public AreaBase(ConvertToEntity entityRef, bool autoClearHitOnExit)
        {
            _entityRef = entityRef;
            _autoClearHitOnExit = autoClearHitOnExit;
        }

        public void Init<T>(Context context) where T : struct
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            var world = context.Resolve<EcsWorld>();
            _contacts.SetPools<ActiveArea<T>>(_entityRef, world);
            _receivers.SetPools<ReceiversInArea<T>>(_entityRef, world);
            _leavers.SetPools<LeaversFromArea<T>>(_entityRef, world);
        }

        internal void OnDisable()
        {
            _contacts.Clear();
            _receivers.Clear();
            _potentialLeavers.Clear();
            _leavers.Clear();
            _processed.Clear();
            ReceiversClearCount = 0;
        }

        public void ResetTargets()
        {
            _processed.Clear();
            _receivers.Clear();
            _receivers.AddEntities(_contacts);
            ReceiversClearCount = 0;
        }

        public void ReceiversClear()
        {
            _receivers.Clear();
            ReceiversClearCount++;
        }

        public bool TryGetEntityTriggerEnter(Collider2D col, out ConvertToEntity entityRef)
        {
            entityRef = TriggerCache.ExtractEntity(col);

            if (entityRef is null || entityRef.RawEntity == -1)
                return false;

            _contacts.Add(entityRef, col);

            if (!_processed.entities.Contains(entityRef))
                _receivers.Add(entityRef, col);

            _leavers.Del(entityRef);
            return true;
        }

        public bool TryGetEntityTriggerExit(Collider2D col, out ConvertToEntity entityRef)
        {
            entityRef = TriggerCache.ExtractEntity(col);

            if (entityRef is null || entityRef.RawEntity == -1)
                return false;

            _contacts.Del(entityRef, col);
            _receivers.Del(entityRef, col);

            if (_potentialLeavers.Del(entityRef, col))
                _leavers.Add(entityRef);

            if (_autoClearHitOnExit)
                _processed.Del(entityRef);

            return true;
        }

        public int GetFirst() => _contacts.entities.First().Key.RawEntity;

        public void ForEachReceivers(Action<int> callback, bool calculateTriggerPoint = false)
        {
            foreach (var (entityRef, set) in _receivers.entities)
            {
                if (calculateTriggerPoint)
                    TriggerPoint = set.SumBy(i => i.transform.position) / set.Count;

                callback.Invoke(entityRef.RawEntity);
            }
        }

        public void ForEachInArea(Action<int> callback)
        {
            foreach (var convertToEntity in _contacts.entities.Keys)
                callback.Invoke(convertToEntity.RawEntity);
        }

        public void ForEachPotentialLeavers(Action<int> callback)
        {
            foreach (var entityRef in _potentialLeavers.entities.Keys)
                callback.Invoke(entityRef.RawEntity);
        }

        public void ForEachLeavers(Action<int> callback)
        {
            foreach (var entityRef in _leavers.entities)
                callback.Invoke(entityRef.RawEntity);

            _leavers.Clear();
        }

        public void WriteReceiversToPotentialLeavers() => _potentialLeavers.AddEntities(_receivers);

        public void WriteReceiversToHits() => _processed.AddEntities(_receivers);
    }
}
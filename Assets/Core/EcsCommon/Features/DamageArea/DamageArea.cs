using System;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class DamageArea : MonoConstruct
    {
        [SerializeField] private bool _autoClearHitOnExit;
        private AreaBase _areaLogic;
        private Action<ConvertToEntity> _onEntityDeleted;
        private readonly Glossary<int> _collidersCount = new();
        private readonly Glossary<(float scale, int priority)> _damageScales = new();
        private Action<int> _forEachEntity;
        private Action<int, float> _callback;

        public Vector2 TriggerPoint => _areaLogic.TriggerPoint;
        public int ReceiversClearCount => _areaLogic.ReceiversClearCount;

        private void Awake()
        {
            _onEntityDeleted = OnEntityDeleted;
            var entityRef = GetComponentInParent<ConvertToEntity>();
            Assert.IsNotNull(entityRef);
            _areaLogic = new(entityRef, _autoClearHitOnExit);
            _forEachEntity = ForEachEntity;
        }

        public void Init<T>(T _) where T : struct => _areaLogic.Init<T>(context);

        public void ResetTargets() => _areaLogic.ResetTargets();

        public void ReceiversClear() => _areaLogic.ReceiversClear();

        public void TriggerEnter(Component col, float damageScale, int priority)
        {
            if (!_areaLogic.TryGetEntityTriggerEnter(col, out var entityRef))
                return;

            int id = entityRef.RawEntity;
            ref var count = ref _collidersCount.TryGetValueRef(id, out var exist);
            if (exist)
            {
                count++;
                ref var value = ref _damageScales.GetValue(id);
                if (value.priority > priority)
                    return;

                value.priority = priority;
                value.scale = damageScale;

                return;
            }

            _collidersCount.Add(id) = 1;
            _damageScales.Add(id, (damageScale, priority));
            entityRef.BeforeEntityDeleted += _onEntityDeleted;
        }

        public void TriggerExit(Component col)
        {
            if (!_areaLogic.TryGetEntityTriggerExit(col, out var entityRef))
                return;

            int id = entityRef.RawEntity;
            ref var count = ref _collidersCount.TryGetValueRef(id, out var exist);
            if (exist && --count != 0)
                return;

            _collidersCount.Remove(id);
            _damageScales.Remove(id);
            entityRef.BeforeEntityDeleted -= _onEntityDeleted;
        }

        public int GetFirst() => _areaLogic.GetFirst();

        public void ForEachReceivers(Action<int, float> callback, bool calculateTriggerPoint = false)
        {
            _callback = callback;
            _areaLogic.ForEachReceivers(_forEachEntity, calculateTriggerPoint);
        }

        private void ForEachEntity(int entity)
        {
            _callback(entity, _damageScales.GetValue(entity).scale);
        }

        public void ForEachInArea(Action<int> callback) =>
            _areaLogic.ForEachInArea(callback);

        public void ForEachPotentialLeavers(Action<int> callback) =>
            _areaLogic.ForEachPotentialLeavers(callback);

        public void ForEachLeavers(Action<int> callback) =>
            _areaLogic.ForEachLeavers(callback);

        public void WriteReceiversToPotentialLeavers() => _areaLogic.WriteReceiversToPotentialLeavers();

        public void WriteReceiversToHits() => _areaLogic.WriteReceiversToHits();

        private void OnDisable() => _areaLogic.OnDisable();

        private void OnEntityDeleted(ConvertToEntity entityRef)
        {
            int id = entityRef.RawEntity;
            _collidersCount.Remove(id);
            _damageScales.Remove(id);
            entityRef.BeforeEntityDeleted -= _onEntityDeleted;
        }
    }
}
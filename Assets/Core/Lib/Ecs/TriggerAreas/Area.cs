using System;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class Area : MonoConstruct, ITriggerDispatcherTarget2D
    {
        [SerializeField] public bool _autoClearHitOnExit;
        private AreaBase _areaLogic;

        public Vector2 TriggerPoint => _areaLogic.TriggerPoint;
        public int ReceiversClearCount => _areaLogic.ReceiversClearCount;

        private void Awake()
        {
            var entityRef = GetComponentInParent<ConvertToEntity>();
            Assert.IsNotNull(entityRef);
            _areaLogic = new(entityRef, _autoClearHitOnExit);
        }

        public void Init<T>(T _) where T : struct => _areaLogic.Init<T>(context);

        private void OnDisable() => _areaLogic.OnDisable();

        public void ResetTargets() => _areaLogic.ResetTargets();

        public void ReceiversClear() => _areaLogic.ReceiversClear();

        public void OnTriggerEnter2D(Collider2D col) => _areaLogic.TryGetEntityTriggerEnter(col, out _);

        public void OnTriggerExit2D(Collider2D col) => _areaLogic.TryGetEntityTriggerExit(col, out _);

        public int GetFirst() => _areaLogic.GetFirst();

        public void ForEachReceivers(Action<int> callback, bool calculateTriggerPoint = false) =>
            _areaLogic.ForEachReceivers(callback, calculateTriggerPoint);

        public void ForEachInArea(Action<int> callback) =>
            _areaLogic.ForEachInArea(callback);

        public void ForEachPotentialLeavers(Action<int> callback) =>
            _areaLogic.ForEachPotentialLeavers(callback);

        public void ForEachLeavers(Action<int> callback) =>
            _areaLogic.ForEachLeavers(callback);

        public void WriteReceiversToPotentialLeavers() => _areaLogic.WriteReceiversToPotentialLeavers();

        public void WriteReceiversToHits() => _areaLogic.WriteReceiversToHits();
    }
}
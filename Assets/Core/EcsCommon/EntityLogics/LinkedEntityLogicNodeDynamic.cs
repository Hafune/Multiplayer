using System;
using Lib;
using UnityEngine;
using Core.Lib;

namespace Core.ExternalEntityLogics
{
    public class LinkedEntityLogicNodeDynamic : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        private DynamicObjectTracker<AbstractEntityLogic> _tracker;
        private Action<AbstractEntityLogic> _forEachActive;

        private void Awake()
        {
            var logics = transform.GetSelfChildrenComponents<AbstractEntityLogic>(true);
            _tracker = new DynamicObjectTracker<AbstractEntityLogic>(logics);
            _forEachActive = ForEachActive;
        }

        public override void Run(int entity) => _tracker.ForEachActive(_forEachActive);

        private void ForEachActive(AbstractEntityLogic logic) => logic.Run(_convertToEntity.RawEntity);
    }
}
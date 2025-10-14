using System;
using System.Linq;
using Core.Lib;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class EntityLogicNodeDynamic : AbstractEntityLogic
    {
        [SerializeField] private AbstractEntityLogic[] externalLogics;
        private DynamicObjectTracker<AbstractEntityLogic> _tracker;
        private Action<AbstractEntityLogic> _forEachActive;
        private int _entity;

        private void Awake()
        {
            var logics = transform
                .GetSelfChildrenComponents<AbstractEntityLogic>(true)
                .Concat(externalLogics);

            _tracker = new DynamicObjectTracker<AbstractEntityLogic>(logics.ToArray());
            _forEachActive = ForEachActive;
        }

        public override void Run(int entity)
        {
            _entity = entity;
            _tracker.ForEachActive(_forEachActive);
        }

        private void ForEachActive(AbstractEntityLogic logic) => logic.Run(_entity);
    }
}
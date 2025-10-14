using System;
using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class EntityAction : AbstractEntityActionStateful
    {
        [SerializeField] private AbstractEntityLogic _start;
        [SerializeField] private AbstractEntityLogic _update;
        [SerializeField] private AbstractEntityLogic _completeStreaming;
        [SerializeField] private AbstractEntityLogic _cancel;

        private AbstractEntityCondition _condition;
        private DynamicObjectTracker<IActionSubStartLogic> _subStartTracker;
        private DynamicObjectTracker<IActionSubCancelLogic> _subCancelTracker;
        private Action<IActionSubStartLogic> _forSubStart;
        private Action<IActionSubCancelLogic> _forSubCancel;
        private int _entity;
        private EcsPool<ActionUpdateTag> _updateTagPool;

        private void Awake()
        {
            _condition = transform.GetSelfChildrenComponent<AbstractEntityCondition>();

            var subStart = GetComponentsInChildren<IActionSubStartLogic>(true);
            _subStartTracker = new DynamicObjectTracker<IActionSubStartLogic>(subStart);

            var subCancel = GetComponentsInChildren<IActionSubCancelLogic>(true);
            _subCancelTracker = new DynamicObjectTracker<IActionSubCancelLogic>(subCancel);
            _forSubStart = ForSubStart;
            _forSubCancel = ForSubCancel;
            _updateTagPool = context.Resolve<ComponentPools>().ActionUpdate;
#if UNITY_EDITOR
            Assert.IsNotNull(_start);
#endif
        }

        public override bool CheckConditionLogic(int entity) => _condition?.Check(entity) ?? true;

        public override void StartLogic(int entity)
        {
            _start?.Run(entity);
            _entity = entity;
            _subStartTracker.ForEachActive(_forSubStart);

            if (_update is not null)
                _updateTagPool.Add(entity);
        }

        public override void UpdateLogic(int entity) => _update.Run(entity);

        public override void CompleteStreamingLogic(int entity) => _completeStreaming?.Run(entity);

        public override void CancelLogic(int entity)
        {
            _cancel?.Run(entity);
            _entity = entity;
            _subCancelTracker.ForEachActive(_forSubCancel);
            
            if (_update is not null)
                _updateTagPool.Del(entity);
        }

        private void ForSubStart(IActionSubStartLogic logic) => logic.SubStart(_entity);
        private void ForSubCancel(IActionSubCancelLogic logic) => logic.SubCancel(_entity);
    }
}
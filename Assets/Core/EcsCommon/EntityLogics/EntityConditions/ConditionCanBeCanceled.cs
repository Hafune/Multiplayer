using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ConditionCanBeCanceled : AbstractEntityCondition
    {
        [SerializeField] private AbstractEntityActionStateful[] _cancelableActions;
        [SerializeField] private bool _allowSelfIfCanCancelAll;
        private EcsPool<ActionCurrentComponent> _currentPool;
        private EcsPool<ActionCanBeCanceledTag> _canBeCanceledPool;
        private AbstractEntityActionStateful _selfLogic;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _currentPool = pools.ActionCurrent;
            _canBeCanceledPool = pools.ActionCanBeCanceled;
            _selfLogic = GetComponentInParent<AbstractEntityActionStateful>();
        }

        public override bool Check(int entity)
        {
            if (!_canBeCanceledPool.Has(entity))
                return false;

            var logic = _currentPool.Get(entity).logic;
            
            if (_cancelableActions.Length == 0 && (!ReferenceEquals(logic, _selfLogic) || _allowSelfIfCanCancelAll))
                 return true;

            for (int i = 0, iMax = _cancelableActions.Length; i < iMax; i++)
                if (ReferenceEquals(_cancelableActions[i], logic))
                    return true;

            return false;
        }
    }
}
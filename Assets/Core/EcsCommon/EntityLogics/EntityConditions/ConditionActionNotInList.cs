using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ConditionActionNotInList : AbstractEntityCondition
    {
        [SerializeField] private AbstractEntityActionStateful[] _cancelableActions;
        private EcsPool<ActionCurrentComponent> _currentPool;
        
        private void Awake() => _currentPool = context.Resolve<ComponentPools>().ActionCurrent;

        public override bool Check(int entity) => IsCurrentActionInCancelableList(entity);

        private bool IsCurrentActionInCancelableList(int entity)
        {
            var logic = _currentPool.Get(entity).logic;

            for (int i = 0, iMax = _cancelableActions.Length; i < iMax; i++)
                if (ReferenceEquals(_cancelableActions[i], logic))
                    return false;

            return true;
        }
    }
}
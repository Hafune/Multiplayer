using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ConditionCheckCost : AbstractEntityCondition, IActionGenericLogic
    {
        [SerializeField] private bool _isPerSecond;
        private LazyPool _pool;
        private EcsPool<ManaPointValueComponent> _manaPool;
        private bool _isValid;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _manaPool = context.Resolve<ComponentPools>().ManaPointValue;
            _pool = new LazyPool(context);
        }

        public override bool Check(int entity)
        {
            _action.InvokeActionContext(this, entity);
            return _isValid;
        }

        public void GenericRun<T>(int entity)
        {
            var pool = _pool.GetPool<CostValueComponent<T>>();
            var value = pool.Get(entity).value;
            _isValid = _manaPool.Get(entity).value - (_isPerSecond ? value / 2 : value) >= 0;
        }
    }
}
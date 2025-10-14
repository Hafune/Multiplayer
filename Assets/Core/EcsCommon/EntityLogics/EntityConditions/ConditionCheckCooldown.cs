using Core.Components;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ConditionCheckCooldown : AbstractEntityCondition, IActionGenericLogic
    {
        private LazyPool _pool;
        private bool _isValid;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _pool = new LazyPool(context);
        }

        public override bool Check(int entity)
        {
            _action.InvokeActionContext(this, entity);
            return _isValid;
        }

        public void GenericRun<T>(int entity)
        {
            var cooldownPool = _pool.GetPool<CooldownValueComponent<T>>();
            var value = cooldownPool.Get(entity);
            _isValid = Time.time - value.startTime >= value.value;
        }
    }
}
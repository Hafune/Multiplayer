using Core.Components;

namespace Core.ExternalEntityLogics
{
    public class ConditionCheckChargeCost : AbstractEntityCondition, IActionGenericLogic
    {
        private LazyPool _costPool;
        private LazyPool _valuePool;
        private bool _isValid;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _costPool = new LazyPool(context);
            _valuePool = new LazyPool(context);
        }

        public override bool Check(int entity)
        {
            _action.InvokeActionContext(this, entity);
            return _isValid;
        }

        public void GenericRun<T>(int entity)
        {
            var costPool = _costPool.GetPool<ChargeCostValueComponent<T>>();
            var valuePool = _valuePool.GetPool<ChargeValueComponent<T>>();
            
            _isValid = valuePool.Get(entity).value - costPool.Get(entity).value >= 0;
        }
    }
}
using Core.Components;
using Lib;
using Unity.Mathematics;

namespace Core.ExternalEntityLogics
{
    public class ApplyActionChargeCostLogic : AbstractEntityLogic, IActionGenericLogic
    {
        private LazyPool _valuePool;
        private LazyPool _valueMaxPool;
        private LazyPool _costPool;
        private LazyPool _eventPool;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _valuePool = new LazyPool(context);
            _valueMaxPool = new LazyPool(context);
            _costPool = new LazyPool(context);
            _eventPool = new LazyPool(context);
        }

        public override void Run(int entity) => _action.InvokeActionContext(this, entity);

        public void GenericRun<T>(int entity)
        {
            var cost = _costPool.GetPool<ChargeCostValueComponent<T>>().Get(entity).value;
            ref var value = ref _valuePool.GetPool<ChargeValueComponent<T>>().Get(entity);
            var maxValue = _valueMaxPool.GetPool<ChargeMaxValueComponent<T>>().Get(entity).value;
            
            value.value = math.clamp(value.value - cost, 0, maxValue);
            _eventPool.GetPool<EventValueUpdated<ChargeValueComponent<T>>>().AddIfNotExist(entity);
        }
    }
}
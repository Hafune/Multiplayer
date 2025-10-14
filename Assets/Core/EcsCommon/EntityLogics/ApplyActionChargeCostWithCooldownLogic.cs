using Core.Components;
using Lib;
using Unity.Mathematics;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ApplyActionChargeCostWithCooldownLogic : AbstractEntityLogic, IActionGenericLogic
    {
        private LazyPool _cooldownPool;
        private LazyPool _chargePool;
        private LazyPool _chargeMaxPool;
        private LazyPool _costPool;
        private LazyPool _chargeEventPool;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _cooldownPool = new LazyPool(context);
            _chargePool = new LazyPool(context);
            _chargeMaxPool = new LazyPool(context);
            _costPool = new LazyPool(context);
            _chargeEventPool = new LazyPool(context);
        }

        public override void Run(int entity) => _action.InvokeActionContext(this, entity);

        public void GenericRun<T>(int entity)
        {
            var cost = _costPool.GetPool<ChargeCostValueComponent<T>>().Get(entity).value;
            ref var charge = ref _chargePool.GetPool<ChargeValueComponent<T>>().Get(entity);
            var chargeMax = _chargeMaxPool.GetPool<ChargeMaxValueComponent<T>>().Get(entity).value;
            ref var cooldown = ref _cooldownPool.GetPool<CooldownValueComponent<T>>().Get(entity);
            
            var previousCharge = charge.value;
            charge.value = math.clamp(charge.value - cost, 0, chargeMax);
            
            if (previousCharge == chargeMax)
            {
                cooldown.startTime = Time.time - cooldown.value * chargeMax;
                cooldown.lastChargeAddTime = Time.time;
            }

            cooldown.startTime += cooldown.value;
            _chargeEventPool.GetPool<EventValueUpdated<ChargeValueComponent<T>>>().AddIfNotExist(entity);
        }
    }
}
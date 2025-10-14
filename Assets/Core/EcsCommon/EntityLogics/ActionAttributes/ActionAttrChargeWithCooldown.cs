using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrChargeWithCooldown : MonoConstruct, IActionAttr
    {
        [field: SerializeField] public float CooldownValue { get; private set; }
        [field: SerializeField] public int MaxCharges { get; private set; } = 1;
        
        private EcsWorld _world;
        private LazyPool _cooldownPool;
        private LazyPool _chargePool;
        private LazyPool _chargeMaxPool;
        private AttrToggle _attrToggle;

        private void Awake()
        {
            _attrToggle = new AttrToggle(this);
            _world = context.Resolve<EcsWorld>();
            _cooldownPool = new LazyPool(context);
            _chargePool = new LazyPool(context);
            _chargeMaxPool = new LazyPool(context);
        }

        private void OnEnable() => _attrToggle.OnEnable(true);

        private void OnDisable() => _attrToggle.OnDisable(false);

        public void Setup<T>(int entity)
        {
            _world.GetPool<BaseValueComponent<CooldownValueComponent<T>>>().Add(entity).baseValue = CooldownValue;
            _chargePool.GetPool<ChargeValueComponent<T>>().Add(entity);
            _world.GetPool<BaseValueComponent<ChargeMaxValueComponent<T>>>().Add(entity).baseValue = MaxCharges;
            _chargeMaxPool.GetPool<ChargeMaxValueComponent<T>>().Add(entity);
            _world.GetPool<BaseValueComponent<ChargeCostValueComponent<T>>>().Add(entity).baseValue = 1;
            _world.GetPool<ChargeCostValueComponent<T>>().Add(entity);
            ref var cooldown = ref _cooldownPool.GetPool<CooldownValueComponent<T>>().Add(entity);
            cooldown.startTime = Time.time;
            cooldown.lastChargeAddTime = Time.time;

            _world.GetPool<EventStartRecalculateAllValues>().AddIfNotExist(entity);
        }
        
        public void Remove<T>(int entity)
        {
            _world.GetPool<BaseValueComponent<CooldownValueComponent<T>>>().Del(entity);
            _world.GetPool<ChargeValueComponent<T>>().Del(entity);
            _world.GetPool<BaseValueComponent<ChargeMaxValueComponent<T>>>().Del(entity);
            _world.GetPool<ChargeMaxValueComponent<T>>().Del(entity);
            _world.GetPool<BaseValueComponent<ChargeCostValueComponent<T>>>().Del(entity);
            _world.GetPool<ChargeCostValueComponent<T>>().Del(entity);
            _world.GetPool<CooldownValueComponent<T>>().Del(entity);
        }
    }
}
using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrCharge : MonoConstruct, IActionAttr
    {
        [field: SerializeField] public int MaxCharges { get; private set; } = 1;

        private EcsWorld _world;
        private LazyPool _chargePool;
        private LazyPool _chargeMaxPool;
        private AttrToggle _attrToggle;

        private void Awake()
        {
            _attrToggle = new AttrToggle(this);
            _world = context.Resolve<EcsWorld>();
            _chargePool = new LazyPool(context);
            _chargeMaxPool = new LazyPool(context);
        }

        private void OnEnable() => _attrToggle.OnEnable(true);

        private void OnDisable() => _attrToggle.OnDisable(false);

        public void Setup<T>(int entity)
        {
            _chargePool.GetPool<ChargeValueComponent<T>>().Add(entity).value = MaxCharges;
            _world.GetPool<BaseValueComponent<ChargeMaxValueComponent<T>>>().Add(entity).baseValue = MaxCharges;
            _chargeMaxPool.GetPool<ChargeMaxValueComponent<T>>().Add(entity).value = MaxCharges;
            _world.GetPool<ChargeCostValueComponent<T>>().Add(entity).value = 1;

            _world.GetPool<EventStartRecalculateAllValues>().AddIfNotExist(entity);
        }

        public void Remove<T>(int entity)
        {
            _world.GetPool<ChargeValueComponent<T>>().Del(entity);
            _world.GetPool<BaseValueComponent<ChargeMaxValueComponent<T>>>().Del(entity);
            _world.GetPool<ChargeMaxValueComponent<T>>().Del(entity);
            _world.GetPool<ChargeCostValueComponent<T>>().Del(entity);
        }
    }
}
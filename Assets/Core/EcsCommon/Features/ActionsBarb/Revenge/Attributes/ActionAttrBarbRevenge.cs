using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrBarbRevenge : MonoConstruct, IActionAttr
    {
        [SerializeField] private BarbRevengeComponent _value;
        private EcsPool<BarbRevengeComponent> _pool;
        private LazyPool _eventPool;
        private LazyPool _valuePool;
        private LazyPool _valueMaxPool;

        private void Awake()
        {
            _pool = context.Resolve<ComponentPools>().BarbRevenge;
            _eventPool = new LazyPool(context);
            _valuePool = new LazyPool(context);
            _valueMaxPool = new LazyPool(context);
        }

        public void Setup<T>(int entity)
        {
            _value.getCharge = i => _valuePool.GetPool<ChargeValueComponent<T>>().Get(i).value;
            _value.getChargeMax = i => _valueMaxPool.GetPool<ChargeMaxValueComponent<T>>().Get(i).value;
            _value.setCharge = (i, v) =>
            {
                _valuePool.GetPool<ChargeValueComponent<T>>().Get(i).value = v;
                _eventPool.GetPool<EventValueUpdated<ChargeValueComponent<T>>>().AddIfNotExist(i);
            };
            _pool.Add(entity) = _value;
        }

        public void Remove<T>(int entity) => _pool.Del(entity);
    }
}
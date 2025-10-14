using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrCost : MonoConstruct, IActionAttr
    {
        [field: SerializeField] public float Value { get; private set; }
        private EcsWorld _world;
        private AttrToggle _attrToggle;

        private void Awake()
        {
            _attrToggle = new AttrToggle(this);
            _world = context.Resolve<EcsWorld>();
        }

        private void OnEnable() => _attrToggle.OnEnable(true);

        private void OnDisable() => _attrToggle.OnDisable(false);

        public void Setup<T>(int entity)
        {
            _world.GetPool<CostValueComponent<T>>().Add(entity);
            _world.GetPool<BaseValueComponent<CostValueComponent<T>>>().Add(entity).baseValue = Value;
            _world.GetPool<EventStartRecalculateAllValues>().AddIfNotExist(entity);
        }
        
        public void Remove<T>(int entity)
        {
            _world.GetPool<CostValueComponent<T>>().Del(entity);
            _world.GetPool<BaseValueComponent<CostValueComponent<T>>>().Del(entity);
        }
    }
}
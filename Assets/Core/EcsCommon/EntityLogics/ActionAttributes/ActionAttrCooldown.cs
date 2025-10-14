using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrCooldown : MonoConstruct, IActionAttr
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
            _world.GetPool<CooldownValueComponent<T>>().Add(entity).startTime = float.MinValue;
            _world.GetPool<BaseValueComponent<CooldownValueComponent<T>>>().Add(entity).baseValue = Value;
            _world.GetPool<EventStartRecalculateAllValues>().AddIfNotExist(entity);
        }
        
        public void Remove<T>(int entity)
        {
            _world.GetPool<CooldownValueComponent<T>>().Del(entity);
            _world.GetPool<BaseValueComponent<CooldownValueComponent<T>>>().Del(entity);
        }
    }
}
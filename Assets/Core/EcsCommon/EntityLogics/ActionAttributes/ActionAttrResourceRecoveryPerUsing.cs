using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrResourceRecoveryPerUsing : MonoConstruct, IActionAttr
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
            _world.GetPool<ResourceRecoveryPerUsingValueComponent<T>>().Add(entity);
            _world.GetPool<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<T>>>().Add(entity).baseValue = Value;
            _world.GetPool<EventStartRecalculateAllValues>().AddIfNotExist(entity);
        }
        
        public void Remove<T>(int entity)
        {
            _world.GetPool<ResourceRecoveryPerUsingValueComponent<T>>().Del(entity);
            _world.GetPool<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<T>>>().Del(entity);
        }
    }
}
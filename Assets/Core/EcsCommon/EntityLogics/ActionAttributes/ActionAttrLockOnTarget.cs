using Core.Components;
using Leopotam.EcsLite;
using Lib;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrLockOnTarget : MonoConstruct, IActionAttr
    {
        private EcsWorld _world;
        private AttrToggle _attrToggle;

        private void Awake()
        {
            _attrToggle = new AttrToggle(this);
            _world = context.Resolve<EcsWorld>();
        }

        private void OnEnable() => _attrToggle.OnEnable(true);

        private void OnDisable() => _attrToggle.OnDisable(false);

        public void Setup<T>(int entity) =>
            _world.GetPool<ActionAttrControlLockOnTargetTag<T>>().Add(entity);
        
        public void Remove<T>(int entity) =>
            _world.GetPool<ActionAttrControlLockOnTargetTag<T>>().Del(entity);
    }
}
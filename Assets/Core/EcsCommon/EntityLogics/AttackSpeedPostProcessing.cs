using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class AnimationSpeedPostProcessing : AbstractAnimationSpeedPostProcessing
    {
        private EcsPool<AttackSpeedValueComponent> _speedPool;
        private EcsPool<WeaponAttackSpeedValueComponent> _weaponPool;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _speedPool = pools.AttackSpeedValue;
            _weaponPool = pools.WeaponAttackSpeedValue;
        }

        public override float CalculateValue(int entity, float speed)
        {
            var weaponSpeed = _weaponPool.Get(entity).value;
            var attackSpeed = _speedPool.Get(entity).value;
            var total = weaponSpeed + weaponSpeed * attackSpeed;
            return total != 0 ? total : 1f;
        }
    }
}
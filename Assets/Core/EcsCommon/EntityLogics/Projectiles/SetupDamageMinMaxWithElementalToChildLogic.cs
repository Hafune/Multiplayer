using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SetupDamageMinMaxWithElementalToChildLogic : AbstractEntityLogic
    {
        [SerializeField] private ElementalDamageContainer _elementalDamage;
        private EcsPool<BaseValueComponent<DamageMinValueComponent>> _baseDamageMinValue;
        private EcsPool<BaseValueComponent<DamageMaxValueComponent>> _baseDamageMaxValue;
        private EcsPool<DamageMinValueComponent> _damageMinValue;
        private EcsPool<DamageMaxValueComponent> _damageMaxValue;
        private ConvertToEntity _convertToEntity;
        private bool _isInitialized;

        private void Awake()
        {
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
            var pools = context.Resolve<ComponentPools>();
            _baseDamageMinValue = pools.BaseDamageMinValue;
            _baseDamageMaxValue = pools.BaseDamageMaxValue;
            _damageMinValue = pools.DamageMinValue;
            _damageMaxValue = pools.DamageMaxValue;
        }

        public override void Run(int child)
        {
            var damageScale = _elementalDamage.GetScale();

            var parentMinDamage = _damageMinValue.Get(_convertToEntity.RawEntity).value;
            _baseDamageMinValue.Add(child).baseValue = parentMinDamage * damageScale;

            var parentMaxDamage = _damageMaxValue.Get(_convertToEntity.RawEntity).value;
            _baseDamageMaxValue.Add(child).baseValue = parentMaxDamage * damageScale;
        }
    }
}
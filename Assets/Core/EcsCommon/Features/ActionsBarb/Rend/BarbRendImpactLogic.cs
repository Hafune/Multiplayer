using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Core.ExternalEntityLogics
{
    public class BarbRendImpactLogic : AbstractEntityLogic
    {
        [SerializeField] private float _damageScale;
        [SerializeField] private float _duration;
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private ElementalDamageContainer _elementalDamage;
        private ComponentPools _pools;
        private ConvertToEntity _convertToEntity;
        private RelationFunctions<AimComponent, TargetComponent> _aimRelationFunctions;
        private EntityBuilder _entityBuilder;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _entityBuilder = new(context);
            _aimRelationFunctions = new(context);
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
            Assert.IsNotNull(_prefab);
        }

        public override void Run(int aim)
        {
            int owner = _convertToEntity.RawEntity;
            var children = _pools.Node.GetOrInitialize(owner).children;

            foreach (var e in _aimRelationFunctions.EnumerateSelfChilds(aim, _pools.BarbRend))
            {
                if (!children.Contains(e) || _pools.EventRemoveEntity.Has(e))
                    continue;

                UpdateValue(e);
                return;
            }

            transform.GetPositionAndRotation(out var position, out var rotation);
            var child = _entityBuilder.Build(_prefab, position, rotation, null, owner);
            UpdateValue(child);

            _aimRelationFunctions.Connect(aim, child);
        }

        private void UpdateValue(int child)
        {
            int owner = _convertToEntity.RawEntity;
            var damage = _pools.DamageMinValue.Get(owner).value + _pools.DamageMaxValue.Get(owner).value;
            damage /= 2;

            _pools.BarbRend.AddIfNotExist(child);
            var totalDamageScale = _damageScale * _elementalDamage.GetScale();
            ref var value = ref _pools.DamagePerSecond.GetOrInitialize(child);
            value.duration = _duration;
            value.damagePerSecond = totalDamageScale / _duration * damage;
            value.startTime = Time.time;
            value.tickDelay = .3f;
            value.lastTickTime = Time.time - value.tickDelay - value.tickDelay * Random.value * .1f;
            value.showDamage = true;
        }
    }
}
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class BarbRendBloodBathImpactLogic : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntityReference _prefabReference;
        private ComponentPools _pools;
        private ConvertToEntity _convertToEntity;
        private RelationFunctions<AimComponent, TargetComponent> _aimRelationFunctions;
        private EntityBuilder _entityBuilder;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _entityBuilder = new EntityBuilder(context);
            _aimRelationFunctions = new(context);
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
        }

        public override void Run(int aim)
        {
            if (_aimRelationFunctions.TryGetSelfChild(aim, _pools.BarbRend, out int child) &&
                !_pools.EventRemoveEntity.Has(child))
            {
                UpdateValue(child);
                return;
            }

            transform.GetPositionAndRotation(out var position, out var rotation);
            child = _entityBuilder.Build(_prefabReference.Prefab, position, rotation, null,
                _pools.Parent.Get(_convertToEntity.RawEntity).entity);
            UpdateValue(child);

            _aimRelationFunctions.Connect(aim, child);
        }

        private void UpdateValue(int child)
        {
            int owner = _convertToEntity.RawEntity;
            var ownerDamage = _pools.DamagePerSecond.Get(owner);
            ref var value = ref _pools.DamagePerSecond.GetOrInitialize(child);
            value.damagePerSecond = ownerDamage.damagePerSecond;

            _pools.BarbRend.AddIfNotExist(child);
            value.duration = ownerDamage.duration;
            value.startTime = Time.time;
            value.tickDelay = ownerDamage.tickDelay;
            value.lastTickTime = Time.time - value.tickDelay - value.tickDelay * Random.value * .2f;
            value.showDamage = true;
        }
    }
}
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ApplyDamagePerSecondLogic : AbstractEntityLogic
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _tickDelay = .3f;
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private bool _showDamage;

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
        }

        public override void Run(int aim)
        {
            int owner = _convertToEntity.RawEntity;
            var children = _pools.Node.GetOrInitialize(owner).children;

            foreach (var e in _aimRelationFunctions.EnumerateSelfChilds(aim, _pools.DamagePerSecond))
            {
                if (!children.Contains(e) || _pools.EventRemoveEntity.Has(e))
                    continue;

                UpdateValue(e);
                return;
            }

            transform.GetPositionAndRotation(out var position, out var rotation);
            var child = _entityBuilder.Build(_prefab, position, rotation, null, owner);
            _aimRelationFunctions.Connect(aim, child);
            UpdateValue(child);
        }

        private void UpdateValue(int child)
        {
            ref var value = ref _pools.DamagePerSecond.GetOrInitialize(child);
            value.duration = _duration;
            value.damagePerSecond = _pools.DamageValue.Get(_convertToEntity.RawEntity).value;;
            value.startTime = Time.time;
            value.tickDelay = _tickDelay;
            value.lastTickTime = Time.time - value.tickDelay;
            value.showDamage = _showDamage;
        }
    }
}



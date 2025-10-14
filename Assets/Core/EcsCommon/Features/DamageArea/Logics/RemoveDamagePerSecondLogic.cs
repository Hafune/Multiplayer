using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class RemoveDamagePerSecondLogic : AbstractEntityLogic
    {
        private ComponentPools _pools;
        private RelationFunctions<AimComponent, TargetComponent> _aimRelationFunctions;
        private ConvertToEntity _convertToEntity;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _aimRelationFunctions = new(context);
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
        }

        public override void Run(int aim)
        {
            var children = _pools.Node.GetOrInitialize(_convertToEntity.RawEntity).children;

            foreach (var e in _aimRelationFunctions.EnumerateSelfChilds(aim, _pools.DamagePerSecond))
            {
                if (!children.Contains(e) || _pools.EventRemoveEntity.Has(e))
                    continue;

                _pools.EventRemoveEntity.AddIfNotExist(e);
                return;
            }
        }
    }
}
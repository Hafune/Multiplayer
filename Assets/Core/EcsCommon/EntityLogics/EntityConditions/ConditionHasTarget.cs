using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;

namespace Core.ExternalEntityLogics
{
    public class ConditionHasTarget : AbstractEntityCondition
    {
        private EcsPool<TargetComponent> _targetPool;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>(); 
            _targetPool = pools.Target;
        }

        public override bool Check(int entity) => _targetPool.Has(entity);
    }
}
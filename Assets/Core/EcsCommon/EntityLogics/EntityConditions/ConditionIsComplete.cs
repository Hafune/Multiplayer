using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;

namespace Core.ExternalEntityLogics
{
    public class ConditionIsComplete : AbstractEntityCondition
    {
        private EcsPool<ActionCompleteTag> _pool;

        private void Awake() => _pool = context.Resolve<ComponentPools>().ActionComplete;

        public override bool Check(int entity) => _pool.Has(entity);
    }
}
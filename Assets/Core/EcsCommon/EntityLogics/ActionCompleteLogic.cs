using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class ActionCompleteLogic : AbstractEntityLogic
    {
        private EcsPool<ActionCompleteTag> _pool;
        private void Awake() => _pool = context.Resolve<ComponentPools>().ActionComplete;

        public override void Run(int entity) => _pool.AddIfNotExist(entity);
    }
}
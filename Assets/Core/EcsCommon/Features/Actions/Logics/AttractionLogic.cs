using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class AttractionLogic : AbstractEntityLogic
    {
        private EcsPool<EventAttraction> _pool;

        private void Awake() => _pool = context.Resolve<ComponentPools>().EventAttraction;

        public override void Run(int target) => _pool.GetOrInitialize(target).position = transform.position;
    }
}
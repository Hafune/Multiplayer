using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class AddResourceGeneratedLogic : AbstractEntityLogic
    {
        [SerializeField] private float value;
        private EcsPool<EventResourceGenerated> _pool;

        private void Awake() => _pool = context.Resolve<ComponentPools>().EventResourceGenerated;

        public override void Run(int entity) => _pool.GetOrInitialize(entity).value += value;
    }
}
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SetupImpactsToggleSubLogic : MonoConstruct, IActionSubStartLogic, IActionSubCancelLogic
    {
        [SerializeField] private HitImpactComponent _value;
        private HitImpactComponent _lastImpacts;
        private EcsPool<HitImpactComponent> _pool;

        private void Awake() => _pool = context.Resolve<ComponentPools>().HitImpact;

        public void SubStart(int entity)
        {
            _lastImpacts = _pool.Get(entity);
            _pool.Get(entity) = _value;
        }

        public void SubCancel(int entity) => _pool.Get(entity) = _lastImpacts;
    }
}
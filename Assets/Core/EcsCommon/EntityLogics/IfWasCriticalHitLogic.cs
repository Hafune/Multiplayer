using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class IfWasCriticalHitLogic : AbstractEntityLogic 
    {
        [SerializeField] private AbstractEntityLogic _next;
        private EcsPool<EventDamageAreaSelfImpactInfoComponent> _infoPool;

        private void Awake()
        {
            _infoPool = context.Resolve<ComponentPools>().EventDamageAreaSelfImpactInfo;
            Assert.IsNotNull(_next);
        }
        
        public override void Run(int entity)
        {
            if (_infoPool.Get(entity).isCriticalHit)
                _next.Run(entity);
        }
    }
}
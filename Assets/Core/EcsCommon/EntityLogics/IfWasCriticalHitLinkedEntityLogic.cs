using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class IfWasCriticalHitLinkedEntityLogic : AbstractEntityLogic 
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        [SerializeField] private AbstractEntityLogic _next;
        private EcsPool<EventDamageAreaSelfImpactInfoComponent> _infoPool;

        private void Awake()
        {
            _infoPool = context.Resolve<ComponentPools>().EventDamageAreaSelfImpactInfo;
            Assert.IsNotNull(_next);
            Assert.IsNotNull(_convertToEntity);
        }
        
        public override void Run(int entity)
        {
            if (_infoPool.Get(_convertToEntity.RawEntity).isCriticalHit)
                _next.Run(entity);
        }
    }
}
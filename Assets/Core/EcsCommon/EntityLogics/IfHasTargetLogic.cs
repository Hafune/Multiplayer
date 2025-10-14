using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine.Assertions;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class IfHasTargetLogic : AbstractEntityLogic 
    {
        [SerializeField] private AbstractEntityLogic _next;
        private IEcsPool _infoPool;
        private void Awake()
        {
            _infoPool = context.Resolve<ComponentPools>().Target;
            Assert.IsNotNull(_next);
        }
        
        public override void Run(int entity)
        {
            if (_infoPool.Has(entity))
                _next.Run(entity);
        }
    }
}
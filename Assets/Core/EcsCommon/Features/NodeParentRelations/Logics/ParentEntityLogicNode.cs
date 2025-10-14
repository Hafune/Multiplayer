using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class ParentEntityLogicNode : AbstractEntityLogic
    {
        private AbstractEntityLogic[] _logics;
        private EcsPool<ParentComponent> _parentPool;

        private void Awake()
        {
            _logics = transform.GetSelfChildrenComponents<AbstractEntityLogic>(true);
            _parentPool = context.Resolve<ComponentPools>().Parent;
        }

        public override void Run(int entity)
        {
            if (!_parentPool.Has(entity))
                return;
            
            int parent = _parentPool.Get(entity).entity;
            for (int i = 0, iMax = _logics.Length; i < iMax; i++)
                _logics[i].Run(parent);
        }
    }
}
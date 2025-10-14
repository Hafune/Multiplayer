using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class LinkedEntityParentLogicNode : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        private AbstractEntityLogic[] _logics;
        private EcsPool<ParentComponent> _parentPool;

        private void Awake()
        {
            _logics = transform.GetSelfChildrenComponents<AbstractEntityLogic>(true);
            _parentPool = context.Resolve<ComponentPools>().Parent;
#if UNITY_EDITOR
            Assert.IsNotNull(_convertToEntity);
#endif
        }

        public override void Run(int _)
        {
            int entity = _convertToEntity.RawEntity;
            if (!_parentPool.Has(entity))
                return;

            int parent = _parentPool.Get(entity).entity;
            for (int i = 0, iMax = _logics.Length; i < iMax; i++)
                _logics[i].Run(parent);
        }
    }
}
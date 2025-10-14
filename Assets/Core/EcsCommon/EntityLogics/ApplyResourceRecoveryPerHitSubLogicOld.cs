using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    [Obsolete]
    public class ApplyResourceRecoveryPerHitSubLogicOld : MonoConstruct, IActionSubStartLogic, IActionSubCancelLogic
    {
        [SerializeField] private float _value;
        private EcsPool<ResourceRecoveryPerHitComponent> _RestoreResourcePerHitPool;

        private void Awake() => _RestoreResourcePerHitPool = context.Resolve<ComponentPools>().ResourceRecoveryPerHit;

        public void SubStart(int entity) => _RestoreResourcePerHitPool.Add(entity).value = _value;

        public void SubCancel(int entity) => _RestoreResourcePerHitPool.Del(entity);
    }
}
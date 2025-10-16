using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SetVelocityByInfoLogic : AbstractEntityLogic
    {
        [SerializeField] private PositionVelocityInfo _info;
        private EcsPool<Rigidbody2DComponent> _rigidbody;

        private void Awake() => _rigidbody = context.Resolve<ComponentPools>().Rigidbody2D;

        public override void Run(int entity) => _rigidbody.Get(entity).rigidbody.linearVelocity = _info.GetVelocity();
    }
}
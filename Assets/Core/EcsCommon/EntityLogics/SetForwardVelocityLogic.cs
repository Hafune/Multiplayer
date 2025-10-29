using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SetForwardVelocityLogic : AbstractEntityLogic
    {
        [SerializeField] private Vector3 _forwardVelocity;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;

        private void Awake() => _rigidbodyPool = context.Resolve<ComponentPools>().Rigidbody;

        public override void Run(int entity)
        {
            var body = _rigidbodyPool.Get(entity).rigidbody;
            body.linearVelocity = body.rotation * _forwardVelocity;
        }
    }
}
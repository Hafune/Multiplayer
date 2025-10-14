using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class StopVelocity2DLogic : AbstractEntityLogic
    {
        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        public override void Run(int entity) => _pools.Rigidbody.Get(entity).rigidbody.linearVelocity = Vector2.zero;
    }
}
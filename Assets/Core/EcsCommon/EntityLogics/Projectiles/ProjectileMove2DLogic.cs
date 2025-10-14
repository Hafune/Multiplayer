using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ProjectileMove2DLogic : AbstractEntityLogic
    {
        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        public override void Run(int entity) => _pools.Rigidbody.Get(entity).rigidbody.linearVelocity =
            (Vector2)_pools.Position.Get(entity).transform.right * _pools.MoveSpeedValue.Get(entity).value;
    }
}
using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RotateToDesiredPosition2DLogic : AbstractEntityLogic
    {
        private float _time;
        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();
        
        public override void Run(int entity)
        {
            var dest = _pools.MoveDesiredPosition.Get(entity);
            var position = (Vector2)_pools.Position.Get(entity).transform.position;
            var line = dest.position - position;

            var rb = _pools.Rigidbody2D.Get(entity).rigidbody;
            rb.rotation = Vector2.SignedAngle(Vector2.right, line.normalized);
        }
    }
}
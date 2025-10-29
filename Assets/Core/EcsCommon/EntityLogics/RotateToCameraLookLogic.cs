using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RotateToCameraLookLogic : AbstractEntityLogic
    {
        private ComponentPools _pools;
        private Transform _cameraTransform;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _cameraTransform = context.Resolve<Camera>().transform;
        }

        public override void Run(int entity)
        {
            var euler = new Vector3(0, _cameraTransform.eulerAngles.y, 0);
            _pools.Rigidbody.Get(entity).rigidbody.MoveRotation(Quaternion.Euler(euler));
        }
    }
}
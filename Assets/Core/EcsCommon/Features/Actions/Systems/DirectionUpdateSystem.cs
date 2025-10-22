using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class DirectionUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                DirectionUpdateTag,
                RigidbodyComponent
            >> _filter;

        private readonly ComponentPools _pools;
        private readonly Transform _cameraTransform;

        public DirectionUpdateSystem(Context context) => _cameraTransform = context.Resolve<Camera>().transform;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var euler = new Vector3(0, _cameraTransform.eulerAngles.y, 0);

                var rigidbody = _pools.Rigidbody.Get(i).rigidbody;
                rigidbody.rotation = Quaternion.Euler(euler);
            }
        }
    }
}
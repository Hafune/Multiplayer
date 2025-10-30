using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class AnimatorRootMotionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                AnimatorRootMotionComponent,
                RigidbodyComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pools.Rigidbody.Get(i).rigidbody.linearVelocity = _pools.AnimatorRootMotion.Get(i).deltaPosition / Time.fixedUnscaledTime;
        }
    }
}
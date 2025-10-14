using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ProjectileMove2DWithAccelerationLogic : AbstractEntityLogic
    {
        [SerializeField] private AnimationCurve _interpolation;
        [SerializeField] private float _time;
        private float _delta;

        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        private void OnEnable() => _delta = 0;

        public override void Run(int entity)
        {
            _delta = Mathf.Clamp01(_delta + Time.deltaTime / _time);
            _pools.Rigidbody.Get(entity).rigidbody.linearVelocity =
                (Vector2)_pools.Position.Get(entity).transform.right * _interpolation.Evaluate(_delta) * _pools.MoveSpeedValue.Get(entity).value;
        }
    }
}
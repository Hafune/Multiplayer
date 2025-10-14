using Core.Generated;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SmoothMoveToEndPointLogic : AbstractEntityLogic, IActionSubStartLogic
    {
        [SerializeField] private float _smooth = 1f;
        [SerializeField] private float _stopDistance = 1f;
        [SerializeField] private float _velocityAngleOffset = 30f;
        private ComponentPools _pools;
        private float _currentSmooth;
        private Vector2 _velocity;
        private int _velocitySign;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        public void SubStart(int entity)
        {
            _currentSmooth = _smooth + _smooth * (-.2f + Random.value * .4f);
            _velocity = _pools.Rigidbody.Get(entity).rigidbody.linearVelocity;
            var endPosition = _pools.MoveDestination.Get(entity).position;
            var singOffset = ((Vector2)transform.position - endPosition) * Random.Range(.8f, 1.6f);
            _velocitySign = (endPosition + singOffset - (Vector2)transform.position).x.Sign();
        }

        public override void Run(int entity)
        {
            var endPosition = _pools.MoveDestination.Get(entity).position;
            var line = endPosition - (Vector2)transform.position;

            if (line.sqrMagnitude < _stopDistance)
            {
                _pools.ActionComplete.AddIfNotExist(entity);
                return;
            }

            Debug.DrawRay(transform.position, Vector3.right * 100, Color.green);

            Vector2.SmoothDamp(transform.position, endPosition, ref _velocity, _currentSmooth);
            _pools.Rigidbody.Get(entity).rigidbody.linearVelocity =
                _velocity.RotatedBy(_velocityAngleOffset * _velocitySign);
        }
    }
}
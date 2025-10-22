using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class ProjectileLaunchToCameraAimLogic : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private float _force;
        [SerializeField] private AbstractEntityLogic _childPostProcessing;
        private EcsPool<EventWaitInit> _eventWaitInitPool;
        private EntityBuilder _entityBuilder;
        private Transform _cameraTransform;
        
        private Vector3 _gizmoStartPoint;
        private Vector3 _gizmoTargetPoint;

        private void Awake()
        {
            _eventWaitInitPool = context.Resolve<ComponentPools>().EventWaitInit;
            _entityBuilder = new EntityBuilder(context);
            _cameraTransform = context.Resolve<Camera>().transform;
            Assert.IsNotNull(_prefab);
        }

        public override void Run(int entity)
        {
            transform.GetPositionAndRotation(out var position, out var rotation);
            var child = _entityBuilder.Build(_prefab, position, rotation, null, entity);
            if (_force != 0)
            {
                const float MAX_CAST_DISTANCE = 50;
                var targetPoint = _cameraTransform.position + _cameraTransform.forward * MAX_CAST_DISTANCE;
                
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit, MAX_CAST_DISTANCE)) 
                    targetPoint = hit.point;

                var direction = (targetPoint - position).normalized;
                var body = _eventWaitInitPool.Get(child).convertToEntity.GetComponent<Rigidbody>();
                body.linearVelocity = direction * _force;
                
                _gizmoStartPoint = position;
                _gizmoTargetPoint = targetPoint;
            }

            _childPostProcessing?.Run(child);
        }
        
        private void OnDrawGizmos()
        {
            if (_gizmoStartPoint == Vector3.zero) return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(_gizmoStartPoint, _gizmoTargetPoint);
            Gizmos.DrawSphere(_gizmoTargetPoint, 0.1f);
        }
    }
}
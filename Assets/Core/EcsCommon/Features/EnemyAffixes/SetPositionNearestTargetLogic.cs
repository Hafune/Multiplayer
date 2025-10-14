using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SetPositionNearestTargetLogic : AbstractEntityLogic
    {
        [SerializeField] private float _distance = 1f;
        private ConvertToEntity _convertToEntity;
        private EcsPool<TargetComponent> _targetPool;
        private EcsPool<PositionComponent> _transformPool;
        private EcsPool<EventWaitInit> _eventWaitInit;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _targetPool = pools.Target;
            _transformPool = pools.Position;
            _eventWaitInit = pools.EventWaitInit;
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
        }

        public override void Run(int entity)
        {
            var targetPosition = _transformPool.Get(_targetPool.Get(_convertToEntity.RawEntity).entity).transform.position;
            targetPosition += (Vector3)Random.insideUnitCircle.normalized * _distance;
            _eventWaitInit.Get(entity).convertToEntity.transform.position = targetPosition;
        }
    }
}
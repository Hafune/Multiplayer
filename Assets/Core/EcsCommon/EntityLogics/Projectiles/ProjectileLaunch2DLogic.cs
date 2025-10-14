using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class ProjectileLaunch2DLogic : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private float _force;
        [SerializeField] private AbstractEntityLogic _childPostProcessing;
        private EcsPool<EventWaitInit> _eventWaitInitPool;
        private EntityBuilder _entityBuilder;

        private void Awake()
        {
            _eventWaitInitPool = context.Resolve<ComponentPools>().EventWaitInit;
            _entityBuilder = new EntityBuilder(context);
#if UNITY_EDITOR
            Assert.IsNotNull(_prefab);
#endif
        }

        public override void Run(int entity)
        {
            transform.GetPositionAndRotation(out var position, out var rotation);
            var child = _entityBuilder.Build(_prefab, position, rotation, null, entity);
            if (_force != 0)
                _eventWaitInitPool.Get(child).convertToEntity.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector3.right * _force;

            _childPostProcessing?.Run(child);
        }
    }
}
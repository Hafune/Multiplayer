using Core.Components;
using Core.Generated;
using UnityEngine;
using Core.Lib;

namespace Core.ExternalEntityLogics
{
    public class SimpleProjectilesUpdateLogic : AbstractEntityLogic, IActionSubStartLogic
    {
        [SerializeField] private ConvertToEntity _projectilePrefab;
        [SerializeField] private AbstractProjectileEmitters _emitters;
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _shotDelay;

        private int _entity;
        private ComponentPools _pools;
        private float _currentDelay;
        private EntityBuilder _entityBuilder;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _entityBuilder = new EntityBuilder(context);
        }

        public void SubStart(int entity) => _currentDelay = 0f;

        public override void Run(int entity)
        {
            if ((_currentDelay -= Time.deltaTime * _pools.AttackSpeedValue.Get(entity).value) > 0)
                return;

            _currentDelay += _shotDelay;
            _entity = entity;
            _emitters.ForEachEmitters(LaunchOne);
        }

        private void LaunchOne(Transform emitter)
        {
            emitter.GetPositionAndRotation(out var position, out var rotation);
            int child = _entityBuilder.Build(_projectilePrefab, position, rotation, null, _entity);
            _pools.EventWaitInit.Get(child).convertToEntity.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector3.right * _startSpeed;
            _pools.BaseMoveSpeedValue.Add(child).baseValue = _startSpeed;
        }
    }
}
using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;
using Core.Lib;

namespace Core.ExternalEntityLogics
{
    [Obsolete(@"Префаб должен находится непосредственно у точки выстрела 
эмитеры нужны только для сложных выстрелов с большим количеством пуль, 
нужно упростить логику")]
    public class ProjectileLaunch2DWithEmittersLogic : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private AbstractProjectileEmitters _emitters;
        [SerializeField] private float _force;
        private int _entity;
        private EcsPool<EventWaitInit> _eventWaitInitPool;
        private EntityBuilder _entityBuilder;

        private void Awake()
        {
            _eventWaitInitPool = context.Resolve<ComponentPools>().EventWaitInit;
            _entityBuilder = new EntityBuilder(context);
        }

        public override void Run(int entity)
        {
            _entity = entity;
            _emitters.ForEachEmitters(ForEachEmitters);
        }

        private void ForEachEmitters(Transform emitter)
        {
            emitter.GetPositionAndRotation(out var position, out var rotation);
            var child = _entityBuilder.Build(_prefab, position, rotation, null, _entity);

            if (_force == 0)
                return;

            _eventWaitInitPool.Get(child).convertToEntity.GetComponent<Rigidbody2D>().velocity = rotation * Vector3.right * _force;
        }
    }
}
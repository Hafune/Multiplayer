using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class UpdateParameterByDirectionLogic : AbstractEntityLogic
    {
        [SerializeField] private SmoothedVector2ParameterContainer _parameter;
        private EcsPool<MoveDirectionComponent> _moveDirectionPool;

        private void Awake() => _moveDirectionPool = context.Resolve<ComponentPools>().MoveDirection;

        public override void Run(int entity) => _parameter.SmoothedParameter.TargetValue = _moveDirectionPool.Get(entity).inputDirection;
    }
}
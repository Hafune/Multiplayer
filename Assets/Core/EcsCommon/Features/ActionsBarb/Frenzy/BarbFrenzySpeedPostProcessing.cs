using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Systems
{
    public class BarbFrenzySpeedPostProcessing : AbstractAnimationSpeedPostProcessing
    {
        [SerializeField] private float _speedPerStack;
        private EcsPool<BarbFrenzyStackValueComponent> _pool;

        private void Awake() => _pool = context.Resolve<ComponentPools>().BarbFrenzyStackValue;

        public override float CalculateValue(int entity, float speed)
        {
            var c = _pool.Get(entity);
            var bonus = c.value * _speedPerStack;
            return speed + bonus * speed;
        }
    }
}
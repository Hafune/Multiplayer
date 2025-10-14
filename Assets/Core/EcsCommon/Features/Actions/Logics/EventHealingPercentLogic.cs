using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using Unity.Mathematics;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class EventHealingPercentLogic : AbstractEntityLogic
    {
        [SerializeField] private float value;
        private EcsPool<EventHealingPercent> _pool;

        private void Awake() => _pool = context.Resolve<ComponentPools>().EventHealingPercent;

        public override void Run(int entity) => _pool.GetOrInitialize(entity).value += value;
    }
}
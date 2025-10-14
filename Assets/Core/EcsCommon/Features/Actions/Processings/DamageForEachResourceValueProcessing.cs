using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class DamageForEachResourceValueProcessing : MonoConstruct, IDamagePostProcessing
    {
        [SerializeField] private float _percentPerValue;
        private EcsPool<ManaPointValueComponent> _manaPool;

        private void Awake() => _manaPool = context.Resolve<EcsWorld>().GetPool<ManaPointValueComponent>();

        public float PostProcessValue(int entity, float damage) => damage + damage * _manaPool.Get(entity).value * _percentPerValue;
    }
}
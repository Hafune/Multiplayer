using Core.Components;
using Leopotam.EcsLite;
using Lib;

namespace Core.ExternalEntityLogics
{
    public abstract class AbstractDamagePostProcessingLogic<T> : MonoConstruct, IDamagePostProcessing
        where T : struct, IValue
    {
        private EcsPool<T> _damagePercentValuePool;

        private void Awake() => _damagePercentValuePool = context.Resolve<EcsWorld>().GetPool<T>();

        public virtual float PostProcessValue(int entity, float damage) => damage + damage * _damagePercentValuePool.Get(entity).value;
    }
}
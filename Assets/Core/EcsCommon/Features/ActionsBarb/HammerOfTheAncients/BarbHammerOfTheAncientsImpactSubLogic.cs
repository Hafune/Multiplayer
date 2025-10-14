using Core.Generated;

namespace Core.ExternalEntityLogics
{
    public class BarbHammerOfTheAncientsImpactSubLogic : AbstractEntityLogic, IActionSubCancelLogic
    {
        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        public override void Run(int entity)
        {
            var manaValue = _pools.ManaPointValue.Get(entity).value;
            _pools.HitAdditionalCriticalChance.Get(entity).value = manaValue / 5f / 100f;
        }

        public void SubCancel(int entity) => _pools.HitAdditionalCriticalChance.Get(entity).value = 0;
    }
}
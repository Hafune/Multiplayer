using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class MaterialEffectCancelLogic : AbstractEntityLogic
    {
        [SerializeField] private MaterialEffectStartLogic key;
        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        public override void Run(int entity)
        {
            if (_pools.MaterialEffectController.Has(entity))
                _pools.MaterialEffectController.Get(entity).controller.RemoveEffect(key.GetID());
        }
    }
}
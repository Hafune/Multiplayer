using Core.Lib;
using Leopotam.EcsLite;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class UseComponentsSubLogic : MonoConstruct, IActionSubStartLogic, IActionSubCancelLogic
    {
        private BaseMonoProvider[] _monoProviders;
        private EcsWorld _world;

        private void Awake()
        {
            _world = context.Resolve<EcsWorld>();
            _monoProviders = GetComponents<BaseMonoProvider>();
        }

        public void SubStart(int entity)
        {
            for (int i = 0, iMax = _monoProviders.Length; i < iMax; i++)
                _monoProviders[i].Attach(entity, _world);
        }

        public void SubCancel(int entity)
        {
            for (int i = 0, iMax = _monoProviders.Length; i < iMax; i++)
                _monoProviders[i].Del(entity, _world);
        }
    }
}
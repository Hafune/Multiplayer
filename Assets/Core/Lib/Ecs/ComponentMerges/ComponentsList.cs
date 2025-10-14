using System.Linq;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class ComponentsList : MonoConstruct
    {
        [SerializeField] private BaseComponentTemplate[] _componentSO;
        private EcsWorld _world;
        private AbstractBaseProvider[] _baseProviders;
        private BaseMonoProvider[] _baseMonoProviders;

        private void Awake()
        {
            _world = context.Resolve<EcsWorld>();
            _baseProviders = _componentSO.Select(i => i.Build()).ToArray();
            _baseMonoProviders = GetComponents<BaseMonoProvider>();
        }

        public void Attach(int entity)
        {
            for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                _baseProviders[i].Attach(entity, _world);

            for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                _baseMonoProviders[i].Attach(entity, _world);
        }

        public void Del(int entity)
        {
            for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                _baseProviders[i].Del(entity, _world);

            for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                _baseMonoProviders[i].Del(entity, _world);
        }

        public bool Has(int entity)
        {
            for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                if (!_baseProviders[i].Has(entity, _world))
                    return false;

            for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                if (!_baseMonoProviders[i].Has(entity, _world))
                    return false;

            return true;
        }
    }
}
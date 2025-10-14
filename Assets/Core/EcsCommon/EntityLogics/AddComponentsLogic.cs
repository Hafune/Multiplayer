using System.Linq;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class AddComponentsLogic : AbstractEntityLogic
    {
        [SerializeField] private BaseComponentTemplate[] _componentSO;
        [SerializeField] bool _overwriteExisting = true;
        private EcsWorld _world;
        private AbstractBaseProvider[] _baseProviders;
        private BaseMonoProvider[] _baseMonoProviders;

        private void Awake()
        {
            _world = context.Resolve<EcsWorld>();
            _baseProviders = _componentSO.Select(i => i.Build()).ToArray();
            _baseMonoProviders = GetComponents<BaseMonoProvider>();
        }

        public override void Run(int entity)
        {
            if (_overwriteExisting)
            {
                for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                    _baseProviders[i].Attach(entity, _world);

                for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                    _baseMonoProviders[i].Attach(entity, _world);
            }
            else
            {
                for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                    if (!_baseProviders[i].Has(entity, _world))
                        _baseProviders[i].Attach(entity, _world);

                for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                    if (!_baseMonoProviders[i].Has(entity, _world))
                        _baseMonoProviders[i].Attach(entity, _world);
            }
        }
    }
}
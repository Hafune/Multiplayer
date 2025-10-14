using System.Linq;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class DelComponentsLogic : AbstractEntityLogic
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

        public override void Run(int entity)
        {
            for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                _baseProviders[i].Del(entity, _world);
            
            for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                _baseMonoProviders[i].Del(entity, _world);
        }
    }
}
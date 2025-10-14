using System;
using System.Linq;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskAddComponentsNode : MonoConstruct, IMyTask
    {
        [SerializeField] private BaseComponentTemplate[] _componentSO;

        private AbstractBaseProvider[] _baseProviders;
        private BaseMonoProvider[] _baseMonoProviders;

        public bool InProgress => false;

        private void Awake()
        {
            _baseProviders = _componentSO.Select(i => i.Build()).ToArray();
            _baseMonoProviders = GetComponents<BaseMonoProvider>();
        }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            var world = context.Resolve<EcsWorld>();
            var _convertToEntity = payload.Get(CommonPayloadKeys.ConvertToEntity);

            for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                _baseProviders[i].Attach(_convertToEntity.RawEntity, world);

            for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                _baseMonoProviders[i].Attach(_convertToEntity.RawEntity, world);
            
            onComplete?.Invoke();
        }
    }
}
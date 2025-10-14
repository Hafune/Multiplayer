using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using Core.Lib;

namespace Core.Services
{
    public class BossHitPointBarClient : MonoConstruct
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        private BossHitPointBarService _service;
        private EcsPool<LocalUiValue<HitPointValueComponent>> _hitPointValuePool;
        private EcsPool<LocalUiValue<HitPointMaxValueComponent>> _hitPointMaxValuePool;

        private void OnValidate() => _convertToEntity =
            _convertToEntity ? _convertToEntity : GetComponentInParent<ConvertToEntity>();

        private void Awake()
        {
            _service = context.Resolve<BossHitPointBarService>();
            var world = context.Resolve<EcsWorld>();
            _hitPointValuePool = world.GetPool<LocalUiValue<HitPointValueComponent>>();
            _hitPointMaxValuePool = world.GetPool<LocalUiValue<HitPointMaxValueComponent>>();
        }

        private void OnEnable() => _convertToEntity.RegisterInitializeCall(Reload);

        private void OnDisable()
        {
            _convertToEntity.UnRegisterInitializeCall(Reload);
            _service.Hide();
        }

        private void Reload()
        {
            var entity = _convertToEntity.RawEntity;

            if (entity == -1)
                return;

            ref var value = ref _hitPointValuePool.GetOrInitialize(_convertToEntity.RawEntity);
            ref var valueMax = ref _hitPointMaxValuePool.GetOrInitialize(_convertToEntity.RawEntity);
            value.update -= UpdateValue;
            value.update += UpdateValue;
            valueMax.update -= UpdateMaxValue;
            valueMax.update += UpdateMaxValue;

            _service.Show();
        }

        private void UpdateValue<T>(T c) where T : struct, IValue => _service.ChangeValue(c.value);
        private void UpdateMaxValue<T>(T c) where T : struct, IValue => _service.ChangeValueMax(c.value);
    }
}
using System;
using Core.Components;
using Core.Lib;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core
{
    public class LevelUpParticle : MonoConstruct
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        [SerializeField] private SpawnEffect spawnEffect;
        
        private EcsPool<LocalUiValue<EventLevelUpTaken>> _eventPool;
        private Action _reload;
        private Action<EventLevelUpTaken> _run;
        
        private void Awake()
        {
            _eventPool = context.Resolve<EcsWorld>().GetPool<LocalUiValue<EventLevelUpTaken>>();
            _reload = Reload;
            _run = Run;
            _convertToEntity.RegisterInitializeCall(_reload);
        }

        private void OnDestroy() => _convertToEntity.UnRegisterInitializeCall(_reload);

        private void Reload()
        {
            var entity = _convertToEntity.RawEntity;
            ref var value = ref _eventPool.GetOrInitialize(entity);
            value.update += _run;
        }

        private void Run<T>(T c) where T : struct => spawnEffect.Execute();
    }
}
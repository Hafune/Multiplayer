using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using UnityEngine.EventSystems;
using Core.Lib;

namespace Core
{
    [DisallowMultipleComponent, RequireComponent(typeof(ConvertToEntity), typeof(PlayerMouseDetector))]
    public class InteractionTarget : MonoConstruct
    {
        [SerializeField] private PlayerMouseDetector _playerMouseDetector;
        [SerializeField] private ConvertToEntity _convertToEntity;
        private EcsPool<EventInteractionTargetClick> _interactionTargetClickPool;

        private void OnValidate()
        {
            _playerMouseDetector = GetComponent<PlayerMouseDetector>();
            _convertToEntity = GetComponent<ConvertToEntity>();
        }

        private void Awake()
        {
            var world = context.Resolve<EcsWorld>();
            _interactionTargetClickPool = world.GetPool<EventInteractionTargetClick>();
            _playerMouseDetector.PointerDown += SetupEntityAsTarget;
        }

        private void SetupEntityAsTarget(PointerEventData _) => _interactionTargetClickPool.Add(_convertToEntity.RawEntity);
    }
}


using Core.Components;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using UnityEngine.EventSystems;
using Core.Lib;

namespace Core
{
    [DisallowMultipleComponent, RequireComponent(typeof(ConvertToEntity), typeof(PlayerMouseDetector))]
    public class MouseHoverListener : MonoConstruct
    {
        [SerializeField] private PlayerMouseDetector playerMouseDetector;
        [SerializeField] private ConvertToEntity _convertToEntity;
        private EcsPool<MouseHoverTag> _mouseHoverPool;

        private void OnValidate()
        {
            playerMouseDetector = GetComponent<PlayerMouseDetector>();
            _convertToEntity = GetComponent<ConvertToEntity>();
        }

        private void Awake()
        {
            playerMouseDetector.PointerEnter += PointerEnter;
            playerMouseDetector.PointerExit += PointerExit;
        }

        private void Start()
        {
            _mouseHoverPool = context.Resolve<EcsWorld>().GetPool<MouseHoverTag>();
        }

        private void PointerEnter(PointerEventData _)
        {
            if (_convertToEntity.RawEntity > -1)
                _mouseHoverPool.AddIfNotExist(_convertToEntity.RawEntity);
        }

        private void PointerExit(PointerEventData _)
        {
            if (_convertToEntity.RawEntity > -1)
                _mouseHoverPool.DelIfExist(_convertToEntity.RawEntity);
        }
    }
}
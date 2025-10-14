using System;
using Core.Lib;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    [Obsolete("Для OnEnable есть каст, а это слишком абстрактно, удалить если не найдётся реальных причин использовать")]
    public class OnEnableLogicRun : MonoConstruct
    {
        [SerializeField] private AbstractEntityLogic _next;
        private ConvertToEntity _convertToEntity;

        private void Awake()
        {
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
            Assert.IsNotNull(_next);
        }

        private void OnEnable() => _next.Run(_convertToEntity.RawEntity);
    }
}
using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Lib
{
    public class ElementalDamageContainer : MonoBehaviour
    {
        private DynamicObjectTracker<IDamagePostProcessing> _tracker;
        private Action<IDamagePostProcessing> _forEachActive;
        private float _lastCalculationTime;
        private float _damageScale;

        private void Awake()
        {
            var damagePostProcesses = GetComponentsInChildren<IDamagePostProcessing>(true);
            var convertToEntity = GetComponentInParent<ConvertToEntity>();
            _tracker = new(damagePostProcesses);
            _forEachActive = postProcessing => _damageScale = postProcessing.PostProcessValue(convertToEntity.RawEntity, _damageScale);
        }

        public float GetScale()
        {
            float currentTime = Time.unscaledTime;

            if (_lastCalculationTime == currentTime)
                return _damageScale;

            _damageScale = 1;
            _tracker?.ForEachActive(_forEachActive);
            _lastCalculationTime = currentTime;

            return _damageScale;
        }
    }
}
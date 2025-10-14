using Core.Components;
using Lib;
using UnityEngine;
using Core.Lib;
using Cysharp.Threading.Tasks;

namespace Core
{
    public class HitPointBar : MonoConstruct
    {
        [SerializeField] private Transform _slider;
        [SerializeField] private GameObject _container;
        private float _visibleTimer;

        private const float _maxVisibleTime = 10f;

        private ConvertToEntity _entityRef;
        private ValueListener<HitPointValueComponent> _uiHitPointValuePool;
        private ValueListener<HitPointMaxValueComponent> _uiHitPointMaxValuePool;
        private bool _fadeIsActive;
        private bool _isDead;
        private Quaternion _cameraRotation;

        private void Awake()
        {
            _entityRef = GetComponentInParent<ConvertToEntity>();
            _uiHitPointValuePool = new ValueListener<HitPointValueComponent>(_entityRef, _ => Change());
            _uiHitPointMaxValuePool = new ValueListener<HitPointMaxValueComponent>(_entityRef, _ => Change());
            new ValueListener<EventDeath>(_entityRef, _ =>
            {
                _visibleTimer = 0;
                _isDead = true;
            });
            _container.SetActive(false);
            _cameraRotation = context.Resolve<Camera>().transform.rotation;
        }
        
        private void OnEnable() => _isDead = false;

        private void OnDisable()
        {
            _fadeIsActive = false;
            _container.SetActive(false);
        }

        private void FixedUpdate() => transform.rotation = _cameraRotation;

        private void Change()
        {
            if (_isDead)
                return;
            
            var min = _uiHitPointValuePool.Get().value;
            var max = _uiHitPointMaxValuePool.Get().value;
            var percent = min / max;

            if (!float.IsFinite(percent))
                return;

            _slider.localScale = new Vector3(percent, 1, 1);
            _visibleTimer = _maxVisibleTime;

            if (_fadeIsActive || percent == 1f)
                return;

            _container.SetActive(true);
            _fadeIsActive = true;
            FadeOutAsync().Forget();
        }

        private async UniTaskVoid FadeOutAsync()
        {
            while (_fadeIsActive && (_visibleTimer -= Time.deltaTime) > 0)
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);

            if (_fadeIsActive)
            {
                _container.SetActive(false);
                _fadeIsActive = false;
            }
        }
    }
}
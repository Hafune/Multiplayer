using System;
using Animancer;
using Unity.Mathematics;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class PlayWithSpeedPostprocessingLogic : AbstractEntityLogic
    {
        [SerializeField] private int _layer;
        [SerializeField] private AnimationClip _clip;
        [SerializeField, Min(0.05f)] private float _speedScale = 1f;
        
        private AnimancerComponent _animancer;
        private AbstractAnimationSpeedPostProcessing[] _speedPostProcessing;
        private Action<int> _refreshAnimationSpeed;
        private AnimancerState _currentClip;

        private void Awake()
        {
            _animancer = GetComponentInParent<AnimancerComponent>();
            _speedPostProcessing = GetComponentsInChildren<AbstractAnimationSpeedPostProcessing>();
            _refreshAnimationSpeed = RefreshSpeed;
            
            for (var i = 0; i < _speedPostProcessing.Length; i++)
                _speedPostProcessing[i].OnChange = _refreshAnimationSpeed;
        }
        
        public override void Run(int entity)
        {
            _currentClip = _animancer.Layers[_layer].Play(_clip);
            _currentClip.Time = 0;
            _currentClip.Speed = CalculateSpeed(entity);
        }

        private void RefreshSpeed(int entity)
        {
            if (_currentClip is not null && _currentClip.IsActive)
                _currentClip.Speed = CalculateSpeed(entity);
        }

        private float CalculateSpeed(int entity)
        {
            var speed = 0f;
            for (var i = 0; i < _speedPostProcessing.Length; i++) 
                speed = _speedPostProcessing[i].CalculateValue(entity, speed);

            return math.clamp(speed * _speedScale, 0.1f, 5f);
        }
    }
}
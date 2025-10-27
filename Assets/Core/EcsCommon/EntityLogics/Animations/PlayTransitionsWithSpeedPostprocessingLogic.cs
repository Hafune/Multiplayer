using System;
using Animancer;
using Unity.Mathematics;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class PlayTransitionsWithSpeedPostprocessingLogic : AbstractEntityLogic
    {
        [SerializeField] private int _layer;
        [SerializeField, Min(0)] private float _speedScale = 1f;
        [SerializeField] private ClipTransition[] _clips;
        [SerializeField] private ClipTransition[] _loop;
        
        private AnimancerComponent _animancer;
        private AbstractAnimationSpeedPostProcessing[] _speedPostProcessing;
        private Action<int> _refreshAnimationSpeed;
        private AnimancerState _currentClip;
        private float _speed;

        private void Awake()
        {
            _animancer = GetComponentInParent<AnimancerComponent>();
            _speedPostProcessing = GetComponentsInChildren<AbstractAnimationSpeedPostProcessing>();
            _refreshAnimationSpeed = RefreshSpeed;
            
            for (var i = 0; i < _speedPostProcessing.Length; i++)
                _speedPostProcessing[i].OnChange = _refreshAnimationSpeed;
            
            for (int i = 0; i < _clips.Length; i++)
            {
                var next = i + 1;

                if (next < _clips.Length)
                    _clips[i].Events.OnEnd = () =>
                    {
                        _currentClip = _animancer.Layers[_layer].Play(_clips[next]);
                        _currentClip.Speed = _speed;
                    };

                if (next == _clips.Length && _loop.Length != 0)
                    _clips[i].Events.OnEnd = () =>
                    {
                        _currentClip = _animancer.Layers[_layer].Play(_loop[0]);
                        _currentClip.Speed = _speed;
                    };
            }

            for (int i = 0; i < _loop.Length; i++)
            {
                var next = i + 1;

                if (next < _loop.Length)
                    _loop[i].Events.OnEnd = () =>
                    {
                        _currentClip = _animancer.Layers[_layer].Play(_loop[next]);
                        _currentClip.Speed = _speed;
                    };
                else
                    _loop[i].Events.OnEnd = () =>
                    {
                        _currentClip = _animancer.Layers[_layer].Play(_loop[0]);
                        _currentClip.Speed = _speed;
                    };
            }
        }

        public override void Run(int entity)
        {
            // _animancer.Playable.Layers[_layer].CurrentState?.Events?.Clear();

            _currentClip = _animancer.Layers[_layer].Play(_clips.Length != 0 ? _clips[0] : _loop[0]);
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
            _speed = 0f;
            for (var i = 0; i < _speedPostProcessing.Length; i++) 
                _speed = _speedPostProcessing[i].CalculateValue(entity, _speed);

            return math.clamp(_speed * _speedScale, 0.1f, 5f);
        }
    }
}
using Animancer;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class PlayTransitionsLogic : AbstractEntityLogic
    {
        [SerializeField] private int _layer;
        [SerializeField, Min(0)] private float _speedScale = 1f;
        [SerializeField] private ClipTransition[] _clips;
        [SerializeField] private ClipTransition[] _loop;
        [SerializeField] private AnimancerComponent _animancer;

        private void OnValidate() => _animancer = _animancer ? _animancer : GetComponentInParent<AnimancerComponent>();

        private void Awake()
        {
            for (int i = 0; i < _clips.Length; i++)
            {
                var next = i + 1;

                if (next < _clips.Length)
                    _clips[i].Events.OnEnd = () => _animancer.Layers[_layer].Play(_clips[next]);

                if (next == _clips.Length && _loop.Length != 0)
                    _clips[i].Events.OnEnd = () => _animancer.Layers[_layer].Play(_loop[0]);
            }

            for (int i = 0; i < _loop.Length; i++)
            {
                var next = i + 1;

                if (next < _loop.Length)
                    _loop[i].Events.OnEnd = () => _animancer.Layers[_layer].Play(_loop[next]);
                else
                    _loop[i].Events.OnEnd = () => _animancer.Layers[_layer].Play(_loop[0]);
            }
        }

        public override void Run(int entity)
        {
            // _animancer.Playable.Layers[_layer].CurrentState?.Events?.Clear();
            
            var clip = _animancer.Layers[_layer].Play(_clips.Length != 0 ? _clips[0] : _loop[0]);
            clip.Time = 0;
            clip.Speed = _speedScale;
        }
    }
}
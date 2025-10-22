using Animancer;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class PlayTransitionsLogic : AbstractEntityLogic
    {
        [SerializeField] private int _layer = 0;
        [SerializeField] private float _speed = 1;
        [SerializeField] private ClipTransition _clip;
        
        private AnimancerComponent _animancer;

        private void Awake()
        {
            _animancer = GetComponentInParent<AnimancerComponent>();
            Assert.IsNotNull(_clip);
            Assert.IsNotNull(_animancer);
        }

        public override void Run(int entity)
        {
            var state = _animancer.Layers[_layer].Play(_clip);
            state.Time = 0;
            state.Speed = _speed;
        }
    }
}
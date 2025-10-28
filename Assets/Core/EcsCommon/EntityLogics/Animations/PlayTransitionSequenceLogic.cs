using Animancer;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class PlayTransitionSequenceLogic : AbstractEntityLogic
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private TransitionSequence _sequence;
        
        private AnimancerComponent _animancer;

        private void Awake()
        {
            _animancer = GetComponentInParent<AnimancerComponent>();
            Assert.IsNotNull(_sequence);
            Assert.IsNotNull(_animancer);
        }

        public override void Run(int entity)
        {
            var state = _animancer.Play(_sequence);
            state.Speed = _speed;
        }
    }
}
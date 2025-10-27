using Animancer;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class PlayMixerTransition2DLogic : AbstractEntityLogic
    {
        [SerializeField] private MixerTransition2D _mixer;
        private AnimancerComponent _animancer;

        private void Awake() => _animancer = GetComponentInParent<AnimancerComponent>();

        public override void Run(int entity) => 
            _animancer.Play(_mixer);
    }
}
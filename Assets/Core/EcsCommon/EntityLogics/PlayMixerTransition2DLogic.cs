using Animancer;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class PlayMixerTransition2DLogic : AbstractEntityLogic
    {
        [SerializeField] private MixerTransition2D _mixer;
        private SmoothedVector2Parameter _smoothedParameter;
        private AnimancerComponent _animancer;

        private void Awake()
        {
            _animancer = GetComponentInParent<AnimancerComponent>();
            _smoothedParameter = new(_animancer,
                _mixer.ParameterNameX,
                _mixer.ParameterNameY,
                .15f);
        }

        public override void Run(int entity)
        {
            var clip = _animancer.Play(_mixer);
            clip.Time = 0;
        }

        public void SetParameter(Vector2 direction) => _smoothedParameter.TargetValue = direction;
    }
}
using Animancer;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class PlayMixerTransition2DLogic : AbstractEntityLogic
    {
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private MixerTransition2D _mixer;
        [SerializeField] private StringAsset _x;
        [SerializeField] private StringAsset _y;
        private SmoothedVector2Parameter _smoothedParameter;

        private void Awake() => _smoothedParameter = new(_animancer, _x, _y, .15f);

        public override void Run(int entity)
        {
            var clip = _animancer.Play(_mixer);
            clip.Time = 0;
        }

        public void SetParameter(Vector2 direction) => _smoothedParameter.TargetValue = direction;
    }
}
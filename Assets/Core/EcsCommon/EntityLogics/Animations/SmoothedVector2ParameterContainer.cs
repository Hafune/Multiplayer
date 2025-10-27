using Animancer;
using UnityEngine;

namespace Core
{
    public class SmoothedVector2ParameterContainer : MonoBehaviour
    {
        [SerializeField] private StringAsset _parameterNameX;
        [SerializeField] private StringAsset _parameterNameY;
        [SerializeField] private float _smoothTime = .15f;

        public SmoothedVector2Parameter SmoothedParameter { get; private set; }

        private void Awake()
        {
            var animancer = GetComponentInParent<AnimancerComponent>();
            SmoothedParameter = new(animancer,
                _parameterNameX,
                _parameterNameY,
                _smoothTime);
        }
    }
}
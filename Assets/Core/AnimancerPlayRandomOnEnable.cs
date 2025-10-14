using Animancer;
using Lib;
using UnityEngine;

namespace Core
{
    public class AnimancerPlayRandomOnEnable : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private AnimationClip[] _animations;

        private void OnEnable() => _animancer.Play(_animations.Random());
    }
}
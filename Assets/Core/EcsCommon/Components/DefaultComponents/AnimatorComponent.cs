using System;
using Animancer;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct AnimatorComponent
    {
        public Animator animator;
        public AnimancerComponent animancer;
    }
}
using Animancer;
using Core.Components;
using Core.Lib;
using UnityEngine;

[DisallowMultipleComponent]
public class AnimatorProvider : MonoProvider<AnimatorComponent>
{
    private void OnValidate()
    {
        value.animator = value.animator ? value.animator : GetComponentInChildren<Animator>();
        value.animancer = value.animancer ? value.animancer : GetComponentInChildren<AnimancerComponent>();
    }
}
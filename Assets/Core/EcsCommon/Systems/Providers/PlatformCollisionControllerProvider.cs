using Core;
using Core.Components;
using UnityEngine;
using Core.Lib;

[DisallowMultipleComponent]
public class PlatformCollisionControllerProvider : MonoProvider<PlatformCollisionControllerComponent>
{
    private void OnValidate() => value.platformCollisionController = value.platformCollisionController
        ? value.platformCollisionController
        : GetComponentInChildren<PlatformCollisionController>();
}
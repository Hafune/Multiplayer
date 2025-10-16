using Core.Components;
using Core.Lib;
using UnityEngine;

[DisallowMultipleComponent]
public class Rigidbody2DProvider : MonoProvider<Rigidbody2DComponent>
{
    private void OnValidate() => value.rigidbody = value.rigidbody ? value.rigidbody : GetComponent<Rigidbody2D>();
}
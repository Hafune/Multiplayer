using Core.Components;
using Core.Lib;
using UnityEngine;

[DisallowMultipleComponent]
public class Rigidbody2DProvider : MonoProvider<Rigidbody2DComponent>
{
    private void OnValidate() => value.rigidbody2D = value.rigidbody2D ? value.rigidbody2D : GetComponent<Rigidbody2D>();
}
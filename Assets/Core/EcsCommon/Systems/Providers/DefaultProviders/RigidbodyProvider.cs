using Core.Components;
using Core.Lib;
using UnityEngine;

[DisallowMultipleComponent]
public class RigidbodyProvider : MonoProvider<RigidbodyComponent>
{
    private void OnValidate() => value.rigidbody = value.rigidbody ? value.rigidbody : GetComponent<Rigidbody2D>();
}
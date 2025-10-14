using Core.Components;
using Core.Lib;
using UnityEngine;

[DisallowMultipleComponent]
public class PositionProvider : MonoProvider<PositionComponent>
{
    private void OnValidate() => value.transform = value.transform ? value.transform : transform;
}
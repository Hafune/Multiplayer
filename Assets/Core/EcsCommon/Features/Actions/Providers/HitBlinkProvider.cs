using Core;
using Core.Components;
using UnityEngine;
using Core.Lib;

[RequireComponent(typeof(HitBlink))]
public class HitBlinkProvider : MonoProvider<HitBlinkComponent>
{
    private void OnValidate() => value.hitBlink = value.hitBlink ??= GetComponentInChildren<HitBlink>();
}
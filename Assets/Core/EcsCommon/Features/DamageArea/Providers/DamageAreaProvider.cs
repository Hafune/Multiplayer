using Core.Components;
using Core.Lib;
using UnityEngine;
using UnityEngine.Assertions;

[DisallowMultipleComponent]
public class DamageAreaProvider : MonoProvider<DamageAreaComponent>
{
#if UNITY_EDITOR
    private void Awake() => Assert.IsNotNull(value.area, name);
#endif
}
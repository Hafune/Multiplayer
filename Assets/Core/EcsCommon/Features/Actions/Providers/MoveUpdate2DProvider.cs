using Core.Components;
using Core.Lib;
using UnityEngine;

[DisallowMultipleComponent]
public class MoveUpdate2DProvider : MonoProvider<MoveUpdate2DComponent>
{
    [SerializeField] private CircleCollider2D _collider;

    private void OnValidate()
    {
        if (!_collider)
            return;

        value.layer = _collider.gameObject.layer;
        value.radius = _collider.radius * 1.1f;
    }
}
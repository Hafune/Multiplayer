using Core.Components;
using Core.Lib;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
public class BodyRadiusProvider : MonoProvider<BodyRadiusComponent>
{
#if UNITY_EDITOR
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private CircleCollider2D _hitBox;

    [MyButton]
    private void UpdateValue()
    {
        if (!_hitBox)
        {
            CircleCollider2D hitBox = null;
            var colliders = transform.GetComponentsInChildren<CircleCollider2D>();
            foreach (var collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & _targetLayer.value) != 0)
                    hitBox = collider;
            }
            
            _hitBox = hitBox;
        }

        if (!_hitBox)
        {
            Debug.LogError("Коллайдерне найден");
            return;
        }

        value.radius = _hitBox.radius * _hitBox.transform.lossyScale.x;
        EditorUtility.SetDirty(this);
    }
#endif
}
using Lib;
using UnityEngine;

[ExecuteAlways]
public class EditorSetChildrenOffsetByOrder : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private Vector3 _offset;

    private void OnEnable()
    {
        if (Application.isPlaying)
            enabled = false;
    }

    private void Update()
    {
        var offset = Vector3.zero - _offset;
        
        foreach (Transform child in transform)
        {
            var bounds = new Bounds();
            var renderers = child.GetComponentsInChildren<MeshRenderer>();

            if (renderers.Length > 0)
                bounds = renderers[0].bounds;
            
            renderers.ForEach(r => bounds.Encapsulate(r.bounds));
            child.position = offset += _offset + new Vector3(bounds.size.x, 0, 0f);
        }
    }
#endif
}
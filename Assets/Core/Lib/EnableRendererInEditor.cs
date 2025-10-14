using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    [ExecuteInEditMode]
    public class EnableRendererInEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Renderer _component;

        private void OnEnable()
        {
            if (!Application.isPlaying && transform.root.gameObject.scene.buildIndex == -1)
                _component.enabled = true;
        }

        private void OnDisable()
        {
            if (!Application.isPlaying && transform.root.gameObject.scene.buildIndex == -1)
            {
                _component.enabled = false;
                EditorUtility.SetDirty(_component);
            }
        }

#endif
    }
}
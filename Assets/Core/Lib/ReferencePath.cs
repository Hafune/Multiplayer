using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/Content/" + nameof(ReferencePath))]
    public class ReferencePath : ScriptableObject
    {
        [field: SerializeField] public string Path { get; private set; } = "";

        public GameObject Find(Transform transform) => transform.Find(Path)?.gameObject;

        public static implicit operator string(ReferencePath reference) => reference.Path;

#if UNITY_EDITOR
        [SerializeField] private GameObject _prefab;
        [SerializeField, ReadOnly] private GameObject reference;
        [SerializeField, HideInInspector] private bool _hasReference;

        public void SetPrefabPart(GameObject prefab, string path)
        {
            _prefab = prefab;
            Path = path;

            Refresh(_prefab);
        }

        private void OnEnable() => PathReferencePrefabPostprocessor.callback += RefreshPost;

        private void OnDisable() => PathReferencePrefabPostprocessor.callback -= RefreshPost;

        private void OnValidate()
        {
            Refresh(_prefab);

            if (IsDestroyed(reference))
                LogMissingReference();

            if (reference is null && !string.IsNullOrEmpty(Path))
                Debug.LogWarning("референс имеет путь но не имеет объекта: " + Path, this);
        }

        private void DelayedRename()
        {
            var newName = "Path_" + reference.name;
            var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssetIfDirty(this);
        }

        private void RefreshPost(GameObject go) => Refresh(go);

        private void Refresh(GameObject prefab)
        {
            if (!_prefab || _prefab != prefab || IsDestroyed(this))
                return;

            Path = Path.Trim();
            GameObject obj = null;

            if (prefab)
                obj = prefab.transform.Find(Path)?.gameObject;

            if (obj)
                reference = obj;

            HierarchyChanged();
            
            if (reference is null || IsDestroyed(reference))
                return;

            var newName = "Path_" + reference.name;

            if (IsDestroyed(this) || name == newName)
                return;

            EditorApplication.delayCall += DelayedRename;
        }

        private void HierarchyChanged()
        {
            if (!_prefab)
                return;

            bool wasLink = _hasReference; 
            if (!reference)
            {
                reference = _prefab.transform.Find(Path)?.gameObject;
                _hasReference = reference;
                EditorUtility.SetDirty(this);
            }
            else if (!_prefab.transform.Find(Path))
            {
                Path = GetPath(reference);
                EditorUtility.SetDirty(this);
            }

            if (reference is not null || wasLink && !_hasReference)
                return;

            LogMissingReference();
            _hasReference = false;
            EditorUtility.SetDirty(this);
        }

        private bool IsDestroyed(Object o) => o is not null && !o;
        
        private void LogMissingReference() => Debug.LogError("Потеряна ссылка на объект: " + Path, this);
        
        public static string GetPath(GameObject obj)
        {
            string path = obj.name;

            while (obj.transform.parent != null && obj.transform.parent.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = obj.name + "/" + path;
            }

            return path;
        }
#endif
    }
}
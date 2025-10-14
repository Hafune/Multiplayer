#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class PathReferencePrefabPostprocessor : AssetPostprocessor
    {
        public static Action<GameObject> callback;

        static void OnPostprocessAllAssets(string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (string path in importedAssets)
            {
                if (!path.EndsWith(".prefab"))
                    continue;
                
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                
                if (prefab is not null && !prefab)
                    return;
                
                callback?.Invoke(prefab);
            }
        }
    }
}
#endif

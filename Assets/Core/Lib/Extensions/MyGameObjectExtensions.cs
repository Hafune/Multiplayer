using System;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lib
{
    public static class MyGameObjectExtensions
    {
        public static string GetPath(this GameObject obj)
        {
            string path = obj.name;

            while (obj.transform.parent != null && obj.transform.parent.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = obj.name + "/" + path;
            }

            return path;
        }

        public static void Rename(this GameObject go, string name)
        {
#if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(go))
                return;

            go.name = name;

            EditorUtility.SetDirty(go);
#endif
        }

        public static void UpdateTemplateId(this GameObject go, int templateId, Action<int> callback)
        {
#if UNITY_EDITOR
            if (Application.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                callback(templateId);
                return;
            }

            EditorApplication.delayCall += () =>
            {

                if (go.IsDestroyed())
                    return;
                
                var path = AssetDatabase.GetAssetPath(go.transform.root.gameObject);
                var origin = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (origin != go.transform.root.gameObject)
                {
                    callback(templateId);
                    return;
                }

                var guid = AssetDatabase.AssetPathToGUID(path);
                var TemplateId = Animator.StringToHash(guid);

                if (templateId == TemplateId)
                {
                    callback(templateId);
                    return;
                }

                EditorUtility.SetDirty(go);
                callback(TemplateId);
            };
#endif
        }

        public static void UpdateTemplateId(this ScriptableObject go, int templateId, Action<int> callback)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                callback(templateId);
                return;
            }

            EditorApplication.delayCall += () =>
            {
                if (go.IsDestroyed())
                    return;
                
                var path = AssetDatabase.GetAssetPath(go);
                var origin = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (origin != go)
                {
                    callback(templateId);
                    return;
                }

                var guid = AssetDatabase.AssetPathToGUID(path);
                var TemplateId = Animator.StringToHash(guid);

                if (templateId == TemplateId)
                {
                    callback(templateId);
                    return;
                }

                EditorUtility.SetDirty(go);
                callback(TemplateId);
            };
#endif
        }
    }
}
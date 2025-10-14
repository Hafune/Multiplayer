namespace Core.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class HiddenObjectsTools
    {
        [MenuItem("Auto/Unhide All Objects In Active Scene")]
        static void UnhideAllInScene()
        {
            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();
            int count = 0;

            foreach (var root in rootObjects)
            {
                foreach (var go in root.GetComponentsInChildren<Transform>(true))
                {
                    if (go.gameObject.hideFlags != HideFlags.None)
                    {
                        go.gameObject.hideFlags = HideFlags.None;
                        EditorUtility.SetDirty(go.gameObject);
                        count++;
                    }
                }
            }

            Debug.Log($"Unhid {count} objects in scene '{scene.name}'.");
        }
    }
}
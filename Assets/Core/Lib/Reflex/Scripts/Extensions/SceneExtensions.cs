using System;
using System.Collections.Generic;
using System.Linq;
using Reflex.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace Reflex
{
    internal static class SceneExtensions
    {
        internal static IEnumerable<T> All<T>(this Scene scene) where T : Component
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                var root = StageUtility.GetStage(scene).FindComponentsOfType<Transform>();
                return root.SelectMany(transform => transform.gameObject.GetComponentsInChildren<T>(true))
                    .Where(m => m != null); // Skip missing scripts
            }
#endif
            return scene
                .GetRootGameObjects()
                .SelectMany(gameObject => gameObject.GetComponentsInChildren<T>(true))
                .Where(m => m != null); // Skip missing scripts
        }

        internal static bool TryFindAtRootObjects<T>(this Scene scene, out T finding) where T : Component
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                var value = StageUtility.GetStage(scene).FindComponentOfType<T>();

                if (value)
                {
                    finding = value;
                    return true;
                }

                finding = default;
                return false;
            }
#endif
            var roots = scene.GetRootGameObjects();

            foreach (var root in roots)
            {
                if (root.TryGetComponent<T>(out finding))
                {
                    return true;
                }
            }

            finding = default;
            return false;
        }

        internal static IDisposable OnUnload(this Scene scene, Action callback)
        {
            var gameObject = new GameObject("SceneUnloadHook");
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            SceneManager.MoveGameObjectToScene(gameObject, scene);
            var hook = gameObject.AddComponent<MonoBehaviourEventHook>();
            return hook.OnDestroyEvent.Subscribe(() => callback?.Invoke());
        }
    }
}
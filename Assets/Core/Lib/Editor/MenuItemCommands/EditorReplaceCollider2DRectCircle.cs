#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public static class EditorReplaceCollider2DRectCircle
    {
        [MenuItem("Auto/Change collider type %r")]
        private static void Execute2d()
        {
            Debug.Log($"(Ctrl+R) Change collider type");

            var col = Selection.gameObjects
                .Select(o => o.GetComponent<Collider2D>())
                .FirstOrDefault(c => c != null);

            if (col == null)
            {
                Execute3d();
                return;
            }

            Undo.RecordObject(col, "");
            Collider2D newCollider;
            var enable = col.enabled;
            var isTrigger = col.isTrigger;
            var index = col.gameObject.GetComponents<Component>().ToList().IndexOf(col);

            switch (col)
            {
                case BoxCollider2D:
                    newCollider = Undo.AddComponent<CircleCollider2D>(col.gameObject);
                    break;
                case CircleCollider2D:
                    newCollider = Undo.AddComponent<CapsuleCollider2D>(col.gameObject);
                    break;
                case CapsuleCollider2D:
                    newCollider = Undo.AddComponent<PolygonCollider2D>(col.gameObject);
                    break;
                case PolygonCollider2D:
                    newCollider = Undo.AddComponent<BoxCollider2D>(col.gameObject);
                    break;
                default:
                    throw new Exception("Swap object not found");
            }

            var go = col.gameObject;
            newCollider.enabled = enable;
            newCollider.isTrigger = isTrigger;
            
            while (true)
            {
                var currentComponents = go.GetComponents<Component>().ToList();
                var currentIndex = currentComponents.IndexOf(newCollider);

                if (index == currentIndex)
                    break;

                bool moveCompleted = true;

                if (index < currentIndex)
                    moveCompleted = UnityEditorInternal.ComponentUtility.MoveComponentUp(newCollider);
                if (index > currentIndex)
                    moveCompleted = UnityEditorInternal.ComponentUtility.MoveComponentDown(newCollider);

                if (moveCompleted)
                    continue;

                Debug.Log("Не удалось переместить скрипт");
                return;
            }
            
            Undo.DestroyObjectImmediate(col);
            EditorUtility.SetDirty(go);
        }
        
        private static void Execute3d()
        {
            var col = Selection.gameObjects
                .Select(o => o.GetComponent<Collider>())
                .FirstOrDefault(c => c != null);

            if (col == null)
                return;

            Undo.RecordObject(col, "");
            Collider newCollider;
            var enable = col.enabled;
            var isTrigger = col.isTrigger;
            var index = col.gameObject.GetComponents<Component>().ToList().IndexOf(col);

            switch (col)
            {
                case BoxCollider:
                    newCollider = Undo.AddComponent<SphereCollider>(col.gameObject);
                    break;
                case SphereCollider:
                    newCollider = Undo.AddComponent<CapsuleCollider>(col.gameObject);
                    break;
                case CapsuleCollider:
                    newCollider = Undo.AddComponent<BoxCollider>(col.gameObject);
                    break;
                default:
                    throw new Exception("Swap object not found");
            }

            var go = col.gameObject;
            newCollider.enabled = enable;
            newCollider.isTrigger = isTrigger;
            
            while (true)
            {
                var currentComponents = go.GetComponents<Component>().ToList();
                var currentIndex = currentComponents.IndexOf(newCollider);

                if (index == currentIndex)
                    break;

                bool moveCompleted = true;

                if (index < currentIndex)
                    moveCompleted = UnityEditorInternal.ComponentUtility.MoveComponentUp(newCollider);
                if (index > currentIndex)
                    moveCompleted = UnityEditorInternal.ComponentUtility.MoveComponentDown(newCollider);

                if (moveCompleted)
                    continue;

                Debug.Log("Не удалось переместить скрипт");
                return;
            }
            
            Undo.DestroyObjectImmediate(col);
            EditorUtility.SetDirty(go);
        }
    }
}
#endif
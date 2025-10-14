#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public static class SelectionTools
    {
        [MenuItem("Auto/Selections/Sort GameObjects By Name")]
        private static void SortGameObjectsByName()
        {
            if (!Selection.activeGameObject)
                return;

            int index = Selection.gameObjects.Min(i => i.transform.GetSiblingIndex());
            foreach (var go in Selection.gameObjects.OrderBy(i => i.name))
            {
                go.transform.SetSiblingIndex(index++);
                EditorUtility.SetDirty(go);
            }
        }

        [MenuItem("Auto/Selections/Merge By Distance")]
        public static void MergeByDistance()
        {
            var selection = Selection.transforms;
            var seen = new HashSet<Vector3>();
            var toDelete = new List<GameObject>();

            foreach (var t in selection)
            {
                var pos = t.position;
                // Округление до тысячной доли для корректного сравнения
                var roundedPos = new Vector3(
                    Mathf.Round(pos.x * 1000f) / 1000f,
                    Mathf.Round(pos.y * 1000f) / 1000f,
                    Mathf.Round(pos.z * 1000f) / 1000f
                );

                if (seen.Contains(roundedPos))
                    toDelete.Add(t.gameObject);
                else
                    seen.Add(roundedPos);
            }

            if (toDelete.Count == 0) return;

            Undo.IncrementCurrentGroup();
            Undo.SetCurrentGroupName("Merge By Distance");
            var group = Undo.GetCurrentGroup();

            foreach (var go in toDelete)
                Undo.DestroyObjectImmediate(go);

            Debug.Log("Удалено " + toDelete.Count);
            Undo.CollapseUndoOperations(group);
        }

        [MenuItem("Auto/Selections/Select Similar In Selection")]
        private static void SelectSimilarInSelection()
        {
            var selection = Selection.gameObjects.ToList();
            if (selection.Count == 0)
                return;

            var last = PrefabUtility.GetCorrespondingObjectFromSource(selection[^1]);

            selection.RemoveAll(s =>
            {
                if (!PrefabUtility.IsPartOfAnyPrefab(s))
                    return false;

                var selectedPrefab = PrefabUtility.GetCorrespondingObjectFromSource(s);
                return selectedPrefab != last;
            });

            Selection.objects = selection.ToArray();
        }

        [MenuItem("Auto/Selections/Convert BoxCollider2D Size|Offset To Transform Values")]
        private static void ResetSelectedBoxColliders()
        {
            int processedCount = 0;

            foreach (GameObject obj in Selection.gameObjects)
            {
                BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();

                if (boxCollider == null)
                    continue;

                Undo.RecordObject(obj.transform, "Reset BoxCollider2D");
                Undo.RecordObject(boxCollider, "Reset BoxCollider2D");

                // Сохраняем текущие локальные параметры
                Vector2 currentOffset = boxCollider.offset;
                Vector2 currentSize = boxCollider.size;
                Vector3 currentLocalScale = obj.transform.localScale;
                Vector3 currentLocalPosition = obj.transform.localPosition;

                // Вычисляем новый локальный scale (коллайдер будет иметь size = (1, 1))
                Vector3 newLocalScale = new Vector3(
                    currentLocalScale.x * currentSize.x,
                    currentLocalScale.y * currentSize.y,
                    currentLocalScale.z
                );

                // Вычисляем смещение локальной позиции из-за offset коллайдера
                // Offset применяется после scale, поэтому умножаем на текущий scale
                Vector3 offsetDelta = new Vector3(
                    currentOffset.x * currentLocalScale.x,
                    currentOffset.y * currentLocalScale.y,
                    0f
                );

                // Применяем изменения
                obj.transform.localScale = newLocalScale;
                obj.transform.localPosition = currentLocalPosition + offsetDelta;

                // Сбрасываем параметры коллайдера
                boxCollider.offset = Vector2.zero;
                boxCollider.size = Vector2.one;

                processedCount++;
            }

            if (processedCount > 0)
            {
                Debug.Log($"Обработано {processedCount} объектов с BoxCollider2D");
            }
            else
            {
                Debug.LogWarning("На выбранных объектах не найдено компонентов BoxCollider2D!");
            }
        }

        [MenuItem("Tools/Reset BoxCollider2D To Defaults", true)]
        private static bool ValidateResetSelectedBoxColliders()
        {
            return Selection.gameObjects.Length > 0;
        }
    }
}
#endif
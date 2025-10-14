#if UNITY_EDITOR
using System.Linq;
using Core.Lib;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public static class AssignAutoJoints
    {
        [MenuItem("Auto/Assign Editor Auto Joints")]
        private static void Assign()
        {
            var sortedObjects = Selection.gameObjects
                .OrderBy(obj => GetHierarchyDepth(obj.transform))
                .ToArray();
            
            foreach (var obj in sortedObjects)
            {
                if (obj == null) continue;

                Undo.RegisterCompleteObjectUndo(obj, "Assign EditorAutoJoint"); // Добавляем в Undo
                
                bool wasActive = obj.activeSelf; // Запоминаем, был ли объект включен
                obj.SetActive(false); // Выключаем объект

                if (!obj.GetComponent<EditorAutoRagdollJoint>()) 
                    obj.AddComponent<EditorAutoRagdollJoint>(); // Добавляем скрипт, если его нет

                obj.SetActive(wasActive); // Включаем обратно, если он был включен
            }

            Debug.Log($"[AssignAutoJoints] Добавлено EditorAutoJoint для {Selection.gameObjects.Length} объектов.");
        }

        private static int GetHierarchyDepth(Transform transform)
        {
            int depth = 0;
            while (transform.parent != null)
            {
                depth++;
                transform = transform.parent;
            }
            return depth;
        }
        
        [MenuItem("Auto/Fit Capsule Collider In To Mesh")]
        private static void Fit()
        {
            Mesh mesh = null;
            GameObject target = null;

            foreach (var o in Selection.gameObjects)
            {
                if (o.TryGetComponent<MeshFilter>(out var meshFilter))
                {
                    mesh = meshFilter.sharedMesh;
                    target = meshFilter.gameObject;
                    break;
                }

                if (o.TryGetComponent<SkinnedMeshRenderer>(out var skinnedMesh))
                {
                    mesh = skinnedMesh.sharedMesh;
                    target = skinnedMesh.gameObject;
                    break;
                }
            }

            if (!mesh || !target || target.GetComponent<MeshCollider>())
                return;

            var meshCollider = target.AddComponent<MeshCollider>();
            meshCollider.hideFlags = HideFlags.DontSave;
            meshCollider.sharedMesh = mesh;

            foreach (var o in Selection.gameObjects)
            {
                if (o.TryGetComponent<CapsuleCollider>(out var capsuleCollider))
                {
                    // Регистрируем изменение перед модификацией
                    Undo.RegisterCompleteObjectUndo(capsuleCollider, "Adjust Capsule Radius");
                    RefreshRadius(capsuleCollider, meshCollider);
                }
            }

            if (Application.isPlaying)
                Object.Destroy(meshCollider);
            else
                Object.DestroyImmediate(meshCollider);
        }

        private static void RefreshRadius(CapsuleCollider capsuleCollider, Collider meshCollider)
        {
            var capsuleTransform = capsuleCollider.transform;
            var capsuleCenter = capsuleTransform.TransformPoint(capsuleCollider.center); // Центр капсулы в мировых координатах
            var axisDirection = GetCapsuleAxisDirection(capsuleCollider.direction, capsuleTransform); // Направление оси капсулы

            var baseDirection = axisDirection == Vector3.up || axisDirection == Vector3.down ? Vector3.right : Vector3.up;
            var perpendicularDirection = Vector3.Cross(axisDirection, baseDirection).normalized;

            const int rayCount = 16;
            const float maxRadius = 5f;
            var minDistance = maxRadius;

            // Выпускаем лучи по окружности, вращая вокруг оси капсулы
            for (var i = 0; i < rayCount; i++)
            {
                var rotatedDirection = Quaternion.AngleAxis(360f / rayCount * i, axisDirection) * perpendicularDirection;

                if (!meshCollider.Raycast(new Ray(capsuleCenter, rotatedDirection), out var hit, maxRadius))
                    continue;

                minDistance = Mathf.Min(minDistance, hit.distance);
            }

            // Устанавливаем радиус капсулы
            capsuleCollider.radius = Mathf.Max(0.1f, minDistance);
            Debug.Log($"Рассчитанный радиус капсулы: {capsuleCollider.radius}");
        }

        // Получаем мировое направление оси капсулы
        private static Vector3 GetCapsuleAxisDirection(int direction, Transform capsuleTransform)
        {
            return direction switch
            {
                0 => capsuleTransform.right, // X-ось
                1 => capsuleTransform.up, // Y-ось
                2 => capsuleTransform.forward, // Z-ось
                _ => Vector3.up
            };
        }
    }
}
#endif
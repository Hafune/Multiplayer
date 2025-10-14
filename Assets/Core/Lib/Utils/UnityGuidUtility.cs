using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib.Utils
{
    /// <summary>
    /// Утилита для получения GUID объектов Unity (компонентов, префабов, ScriptableObject и других ассетов)
    /// </summary>
    public static class UnityGuidUtility
    {
#if UNITY_EDITOR
        /// <summary>
        /// Получить GUID любого объекта Unity, который является ассетом или частью ассета
        /// </summary>
        /// <param name="obj">Объект Unity, для которого нужно получить GUID</param>
        /// <returns>GUID объекта или null, если GUID не найден</returns>
        public static string GetGuid(Object obj)
        {
            if (obj == null)
                return null;

            // Получаем путь к ассету
            string assetPath = AssetDatabase.GetAssetPath(obj);

            // Если объект является компонентом, прикрепленным к GameObject
            if (string.IsNullOrEmpty(assetPath) && obj is Component component)
            {
                // Проверяем, является ли GameObject частью префаба
                PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(component.gameObject);

                if (prefabType != PrefabAssetType.NotAPrefab)
                {
                    // Получаем оригинальный префаб
                    Object prefabParent = PrefabUtility.GetCorrespondingObjectFromSource(component.gameObject);
                    assetPath = AssetDatabase.GetAssetPath(prefabParent != null ? prefabParent : component.gameObject);
                }
            }

            // Если путь все еще пустой, значит объект не является ассетом
            if (string.IsNullOrEmpty(assetPath))
                return null;

            // Преобразуем путь к ассету в GUID
            return AssetDatabase.AssetPathToGUID(assetPath);
        }

        /// <summary>
        /// Получить путь к ассету по его объекту
        /// </summary>
        /// <param name="obj">Объект Unity</param>
        /// <returns>Путь к ассету или null, если путь не найден</returns>
        public static string GetAssetPath(Object obj)
        {
            if (obj == null)
                return null;

            string assetPath = AssetDatabase.GetAssetPath(obj);

            // Если объект является компонентом, прикрепленным к GameObject
            if (string.IsNullOrEmpty(assetPath) && obj is Component component)
            {
                // Проверяем, является ли GameObject частью префаба
                PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(component.gameObject);

                if (prefabType != PrefabAssetType.NotAPrefab)
                {
                    // Получаем оригинальный префаб
                    Object prefabParent = PrefabUtility.GetCorrespondingObjectFromSource(component.gameObject);
                    assetPath = AssetDatabase.GetAssetPath(prefabParent != null ? prefabParent : component.gameObject);
                }
            }

            return assetPath;
        }
#endif
    }

    /// <summary>
    /// Расширение для удобного получения GUID объектов Unity
    /// </summary>
    public static class UnityGuidExtensions
    {
#if UNITY_EDITOR
        /// <summary>
        /// Получить GUID объекта
        /// </summary>
        /// <param name="obj">Объект Unity</param>
        /// <returns>GUID объекта или null, если GUID не найден</returns>
        public static string GetGuid(this Object obj)
        {
            return UnityGuidUtility.GetGuid(obj);
        }

        /// <summary>
        /// Получить путь к ассету объекта
        /// </summary>
        /// <param name="obj">Объект Unity</param>
        /// <returns>Путь к ассету или null, если путь не найден</returns>
        public static string GetAssetPath(this Object obj)
        {
            return UnityGuidUtility.GetAssetPath(obj);
        }
#endif
    }
}
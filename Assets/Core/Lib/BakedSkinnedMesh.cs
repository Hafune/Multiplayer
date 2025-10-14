namespace Core.Lib
{
    using UnityEngine;

    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class BakedSkinnedMesh : MonoBehaviour
    {
        private SkinnedMeshRenderer skinnedMeshRenderer;
        private MeshFilter meshFilter;
        private Mesh bakedMesh;

        [SerializeField] private int updateInterval = 1; // Интервал обновления в кадрах (1 = каждый кадр)
        private int frameCounter;
        private int frameOffset;
        private int lastTotalIds;
        
        private static int totalIds;

        private void OnValidate()
        {
            frameOffset = lastTotalIds % updateInterval;
        }

        private void Start()
        {
            // GetComponent<MeshRenderer>().enabled = false;
            // enabled = false;
            // return;
            lastTotalIds = totalIds;
            totalIds++;
            frameOffset = lastTotalIds % updateInterval;
            // Инициализируем компоненты
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
            skinnedMeshRenderer.enabled = false;

            // Создаём меш для запекания
            bakedMesh = new Mesh();
            meshFilter.mesh = bakedMesh; // Назначаем меш для отображения

            frameCounter = frameOffset;
        }

        private void Update()
        {
            // Пропускаем кадры для оптимизации
            frameCounter++;
            
            if (frameCounter < updateInterval)
                return;

            frameCounter -= updateInterval;

            // Запекаем меш
            skinnedMeshRenderer.BakeMesh(bakedMesh);
        }

        private void OnDestroy()
        {
            // Освобождаем память при уничтожении объекта
            if (bakedMesh != null)
            {
                Destroy(bakedMesh);
            }
        }
    }

}
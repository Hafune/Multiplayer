using UnityEngine;
using System.Collections;

public class TestSmoothDampMultiplePoints : MonoBehaviour
{
    public enum PathType
    {
        Linear,
        CatmullRom
    }

    [Header("Настройки пути")]
    public Transform[] waypoints; // Точки пути
    public float duration = 5f; // Общее время перемещения
    public bool loop = false; // Зацикливание движения
    public bool isLocal = true; // Использовать локальные координаты
    public PathType pathType = PathType.Linear; // Тип пути

    private float[] segmentDurations; // Продолжительность каждого сегмента
    private Vector3[] waypointPositions; // Позиции точек пути

    private void Start()
    {
        if (waypoints.Length > 1)
        {
            InitializePath();
            StartCoroutine(MoveAlongPath());
        }
        else
        {
            Debug.LogWarning("Для PathMover необходимо минимум две точки.");
        }
    }

    void InitializePath()
    {
        int waypointCount = waypoints.Length;
        waypointPositions = new Vector3[waypointCount];

        // Сохранение позиций точек
        for (int i = 0; i < waypointCount; i++)
        {
            waypointPositions[i] = GetPoint(i);
        }

        // Вычисление длин сегментов
        int segmentCount = loop ? waypointCount : waypointCount - 1;
        float totalLength = 0f;
        float[] segmentLengths = new float[segmentCount];

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 p1 = waypointPositions[i];
            Vector3 p2 = waypointPositions[(i + 1) % waypointCount];
            float dist = Vector3.Distance(p1, p2);
            segmentLengths[i] = dist;
            totalLength += dist;
        }

        // Вычисление продолжительности каждого сегмента на основе их длин
        segmentDurations = new float[segmentCount];
        for (int i = 0; i < segmentCount; i++)
        {
            segmentDurations[i] = (segmentLengths[i] / totalLength) * duration;
        }
    }

    IEnumerator MoveAlongPath()
    {
        int waypointCount = waypoints.Length;
        int segmentCount = segmentDurations.Length;
        int currentIndex = 0;

        while (true)
        {
            int p0Index, p1Index, p2Index, p3Index;

            if (loop)
            {
                p0Index = (currentIndex - 1 + waypointCount) % waypointCount;
                p1Index = currentIndex % waypointCount;
                p2Index = (currentIndex + 1) % waypointCount;
                p3Index = (currentIndex + 2) % waypointCount;
            }
            else
            {
                p0Index = Mathf.Clamp(currentIndex - 1, 0, waypointCount - 1);
                p1Index = currentIndex;
                p2Index = Mathf.Clamp(currentIndex + 1, 0, waypointCount - 1);
                p3Index = Mathf.Clamp(currentIndex + 2, 0, waypointCount - 1);
            }

            Vector3 p0 = waypointPositions[p0Index];
            Vector3 p1 = waypointPositions[p1Index];
            Vector3 p2 = waypointPositions[p2Index];
            Vector3 p3 = waypointPositions[p3Index];

            float segmentDuration = segmentDurations[currentIndex % segmentCount];
            float elapsedTime = 0f;

            while (elapsedTime < segmentDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / segmentDuration);
                Vector3 newPosition;

                if (pathType == PathType.Linear)
                {
                    newPosition = Vector3.Lerp(p1, p2, t);
                }
                else if (pathType == PathType.CatmullRom)
                {
                    newPosition = CatmullRomSpline(p0, p1, p2, p3, t);
                }
                else
                {
                    newPosition = Vector3.Lerp(p1, p2, t); // По умолчанию линейное перемещение
                }

                if (isLocal)
                {
                    transform.localPosition = newPosition;
                }
                else
                {
                    transform.position = newPosition;
                }
                yield return null;
            }

            currentIndex++;

            if (currentIndex >= segmentCount)
            {
                if (loop)
                {
                    currentIndex = 0;
                }
                else
                {
                    break;
                }
            }
        }
    }

    Vector3 GetPoint(int index)
    {
        if (isLocal)
        {
            return waypoints[index].localPosition;
        }
        else
        {
            return waypoints[index].position;
        }
    }

    Vector3 CatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Формула сплайна Катмулла–Рома
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }
}
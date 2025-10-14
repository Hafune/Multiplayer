using System;
using Lib;
using UnityEngine;

public class TestFreePosition : MonoBehaviour
{
    public Transform target;
    // public LayerMask _mask;

    public float SafeRadius = 10;
    private readonly RaycastHit2D[] _results = new RaycastHit2D[1];

    private void Update()
    {
        var _mask = 1 << gameObject.layer;
        var position = transform.position;
        var targetPosition = target.position;
        var toCenter = targetPosition - position;

        // Расстояние между объектом и центром окружности
        float distanceToCenter = toCenter.magnitude;
        // Угол направления на центр окружности
        float currentAngle = Mathf.Atan2(toCenter.y, toCenter.x) * Mathf.Rad2Deg;
        float angleOffset = 0;
        float lerpOffset = 360;

        // Угол касательных относительно направления на центр
        float tangentAngle = 60f;

        if (distanceToCenter <= SafeRadius)
        {
            lerpOffset = 0;
            angleOffset = 180;
        }
        else
        {
            tangentAngle = Mathf.Asin(SafeRadius / distanceToCenter) * Mathf.Rad2Deg;
        }

        // Углы касательных прямых
        float minAngle = currentAngle + tangentAngle;
        float maxAngle = currentAngle - tangentAngle;

        int rayCount = 8;
        Span<Vector2> paths = stackalloc Vector2[rayCount];
        Span<float> scores = stackalloc float[rayCount];
        float desiredLength = 20f;
        float desiredDistanceToTarget = SafeRadius;
        int successCount = 0;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(maxAngle + lerpOffset, minAngle, i / (rayCount - 1f));
            var direction = Vector2.right.RotatedBy(angle + angleOffset);
            int hitCount = Physics2D.CircleCastNonAlloc(transform.position,
                1, direction, _results, desiredLength, _mask);

            var length = hitCount == 0 ? desiredLength : _results[0].distance;
            var pathLine = direction * length;
            paths[i] = pathLine;
            var nextPosition = transform.position + (Vector3)pathLine;
            var targetLine = targetPosition - nextPosition;
            float targetLineMagnitude = targetLine.magnitude;
            scores[i] = targetLineMagnitude / desiredDistanceToTarget;

            if (hitCount == 0)
            {
                scores[i] = desiredDistanceToTarget / targetLineMagnitude;

                int barriers = Physics2D.CircleCastNonAlloc(nextPosition,
                    1, targetLine, _results, targetLineMagnitude, _mask);

                if (barriers == 0)
                {
                    successCount++;
                    scores[i] *= 10;
                }
            }

            Debug.DrawRay(position, pathLine, Color.red);
        }

        int count = scores.Length;
        for (int i = 0; i < count - 1; i++)
        for (int j = 0; j < count - i - 1; j++)
            if (scores[j] > scores[j + 1])
            {
                (scores[j], scores[j + 1]) = (scores[j + 1], scores[j]);
                (paths[j], paths[j + 1]) = (paths[j + 1], paths[j]);
            }

        int index = successCount == 0 ? 0 : rayCount - 1;
        var pos = position + (Vector3)paths[index];
        var dir = targetPosition - pos;
        Debug.DrawRay(pos, dir, Color.green);
    }
}
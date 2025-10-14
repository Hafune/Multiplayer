using System.Collections.Generic;
using UnityEngine;

namespace Core.Lib
{
    public static class MyPhysics2DUtility
    {
        private static readonly Vector2[] _directions =
        {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right,
            new Vector2(1, 1).normalized, new Vector2(1, -1).normalized,
            new Vector2(-1, 1).normalized, new Vector2(-1, -1).normalized
        };

        public static Vector2 FindFreePosition(Vector2 center, float distance, LayerMask obstacleLayer)
        {
            var startIndex = Random.Range(0, _directions.Length);

            for (var i = 0; i < _directions.Length; i++)
            {
                var currentIndex = (startIndex + i) % _directions.Length;
                var potentialPosition = center + _directions[currentIndex] * distance;
                var delta = potentialPosition - center;
                var castHit = Physics2D.CapsuleCast(center, new Vector2(1f, 1f), CapsuleDirection2D.Vertical, 0f, delta.normalized,
                    delta.magnitude, obstacleLayer);
                if (castHit.collider == null)
                    return potentialPosition;
            }

            return center; // Не удалось найти место
        }

        public static void FillFreePositions(List<Vector2> positions, Vector2 center, int count, float minDistance, LayerMask obstacleLayer,
            MyRandom random)
        {
            positions.Clear();

            var minDistanceSqr = minDistance * minDistance;
            const int attemptsPerPoint = 32;
            var baseMaxRadius = minDistance * 2f;

            for (var i = 0; i < count; i++)
            {
                var placed = false;
                var maxRadius = baseMaxRadius;

                for (var attempt = 0; attempt < attemptsPerPoint; attempt++)
                {
                    var angle = random.NextFloat(0f, Mathf.PI * 2f);
                    var radius = random.NextFloat(minDistance, maxRadius);
                    var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    var candidate = center + dir * radius;

                    var delta = candidate - center;
                    var castHit = Physics2D.CircleCast(
                        center,
                        0.5f, // Радиус "толщины" луча
                        delta.normalized,
                        delta.magnitude,
                        obstacleLayer
                    );

                    var available = castHit.collider == null;
                    if (!available)
                    {
                        if (attempt < attemptsPerPoint)
                            continue;

                        candidate = Vector2.Lerp(castHit.centroid, center, .2f + .6f * random.NextFloat());
                    }

                    var valid = true;
                    for (var j = 0; j < positions.Count; j++)
                    {
                        var d = positions[j] - candidate;
                        if (d.sqrMagnitude < minDistanceSqr)
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (!valid)
                        continue;

                    positions.Add(candidate);
                    placed = true;
                    break;
                }

                if (!placed)
                    positions.Add(center);
            }
        }
    }
}
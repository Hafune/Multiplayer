using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Lib
{
    public class HeightMapService
    {
        private readonly Dictionary<int2, MyList<HeightMapClient>> _spatialGrid = new();
        private readonly MyList<HeightMapClient> _allClients = new();
        private float _cellSize;
        private bool _initialized;

        public void RegisterClient(HeightMapClient client)
        {
            _allClients.Contains(client);

            if (!_initialized)
            {
                var bounds = client.GetArea();
                _cellSize = Mathf.Max(bounds.size.x, bounds.size.z);
                _initialized = true;
            }

            _allClients.Add(client);
            AddToSpatialGrid(client);
        }

        public void UnregisterClient(HeightMapClient client)
        {
            _allClients.Remove(client);
            RemoveFromSpatialGrid(client);

            if (_allClients.Count == 0)
            {
                _spatialGrid.Clear();
                _initialized = false;
            }
        }

        public float GetHeight(Vector2 position)
        {
            if (!_initialized)
                return 0f;

            var gridPos = GetGridPosition(position.x, position.y);
            var worldPos = new Vector3(position.x, 0, position.y);

            // Проверяем только ячейку и соседние ячейки
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var checkPos = new int2(gridPos.x + x, gridPos.y + y);
                    if (!_spatialGrid.TryGetValue(checkPos, out var clients))
                        continue;

                    foreach (var client in clients)
                    {
                        var bounds = client.GetArea();
                        if (bounds.Contains(worldPos))
                        {
                            var localPos = client.transform.InverseTransformPoint(worldPos);
                            return client.GetHeight(new Vector2(localPos.x, localPos.z));
                        }
                    }
                }
            }

            return 0f;
        }

        private void AddToSpatialGrid(HeightMapClient client)
        {
            var bounds = client.GetArea();
            var minGrid = GetGridPosition(bounds.min.x, bounds.min.z);
            var maxGrid = GetGridPosition(bounds.max.x, bounds.max.z);

            for (var x = minGrid.x; x <= maxGrid.x; x++)
            for (var y = minGrid.y; y <= maxGrid.y; y++)
            {
                var gridPos = new int2(x, y);
                if (!_spatialGrid.TryGetValue(gridPos, out var list))
                {
                    list = new MyList<HeightMapClient>();
                    _spatialGrid[gridPos] = list;
                }

                list.Add(client);
            }
        }

        private void RemoveFromSpatialGrid(HeightMapClient client)
        {
            var bounds = client.GetArea();
            var minGrid = GetGridPosition(bounds.min.x, bounds.min.z);
            var maxGrid = GetGridPosition(bounds.max.x, bounds.max.z);

            for (var x = minGrid.x; x <= maxGrid.x; x++)
            {
                for (var y = minGrid.y; y <= maxGrid.y; y++)
                {
                    var gridPos = new int2(x, y);
                    if (_spatialGrid.TryGetValue(gridPos, out var list))
                    {
                        list.Remove(client);
                        if (list.Count == 0)
                            _spatialGrid.Remove(gridPos);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int2 GetGridPosition(float x, float y)
        {
            return new int2(
                Mathf.FloorToInt(x / _cellSize),
                Mathf.FloorToInt(y / _cellSize)
            );
        }
    }
}
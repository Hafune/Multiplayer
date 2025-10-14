using Lib;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib.Utils
{
    [ExecuteAlways]
    public class HeightMapEditor : MonoBehaviour
    {
        [SerializeField] private float _cellSize = 1f;
        [SerializeField] private float _maxHeight = 10f;
        [SerializeField] private bool _autoUpdate = true;
        [SerializeField] private bool _drawGizmo = true;
        [SerializeField] private HeightMap.CompressedHeightMapData _serializedData;

        private Transform _cornerA;
        private Transform _cornerB;
        private Transform _handlesContainer;
        private readonly List<Transform> _heightHandles = new();
        private int _mapWidth;
        private int _mapHeight;
        private int _lastMapWidth = -1;
        private int _lastMapHeight = -1;
        private Vector3 _mapOrigin;
        private byte[] _heightData;

#if UNITY_EDITOR
        private void OnEnable()
        {
            _lastMapWidth = -1;
            _lastMapHeight = -1;
            CreateCornerHandles();
            EditorApplication.update += UpdateHeightMap;
        }

        private void OnDisable()
        {
            transform.DestroyChildren();
            _heightHandles.Clear();
            EditorApplication.update -= UpdateHeightMap;
        }

        private void CreateCornerHandles()
        {
            if (_cornerA == null)
            {
                var goA = GameObject.CreatePrimitive(PrimitiveType.Cube);
                goA.hideFlags = HideFlags.DontSave;
                goA.name = "HeightMap_CornerA";
                goA.transform.parent = transform;
                goA.transform.localPosition = Vector3.zero;
                goA.transform.localScale = Vector3.one * 0.5f;
                goA.transform.rotation = transform.rotation;
                var renderer = goA.GetComponent<Renderer>();
                renderer.receiveShadows = false;
                renderer.shadowCastingMode = ShadowCastingMode.Off;
                var material = renderer.sharedMaterial;
                renderer.sharedMaterial = Instantiate(material);
                renderer.sharedMaterial.color = Color.red;
                _cornerA = goA.transform;
            }

            if (_cornerB == null)
            {
                var goB = GameObject.CreatePrimitive(PrimitiveType.Cube);
                goB.hideFlags = HideFlags.DontSave;
                goB.name = "HeightMap_CornerB";
                goB.transform.parent = transform;
                goB.transform.localPosition = new Vector3(10f, 0f, 10f);
                goB.transform.localScale = Vector3.one * 0.5f;
                goB.transform.rotation = transform.rotation;
                var renderer = goB.GetComponent<Renderer>();
                renderer.receiveShadows = false;
                renderer.shadowCastingMode = ShadowCastingMode.Off;
                var material = renderer.sharedMaterial;
                renderer.sharedMaterial = Instantiate(material);
                renderer.sharedMaterial.color = Color.blue;
                _cornerB = goB.transform;
            }

            if (_handlesContainer == null)
            {
                var handlesContainer = new GameObject(ObjectNames.NicifyVariableName(nameof(_handlesContainer)));
                handlesContainer.hideFlags = HideFlags.DontSave;
                handlesContainer.transform.parent = transform;
                handlesContainer.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                _handlesContainer = handlesContainer.transform;
            }
        }

        private void UpdateHeightMap()
        {
            if (!_autoUpdate)
                return;

            var positionA = _cornerA.transform.localPosition;
            positionA.y = 0;
            _cornerA.transform.localPosition = positionA;

            var positionB = _cornerB.transform.localPosition;
            positionB.y = 0;
            _cornerB.transform.localPosition = positionB;

            _handlesContainer.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            CalculateMapDimensions();

            if (_lastMapHeight == _mapHeight && _lastMapWidth == _mapWidth)
            {
                ConstrainHandlePositions();
                return;
            }

            Undo.RecordObject(this, "Change HeightMap Size");

            _lastMapWidth = _mapWidth;
            _lastMapHeight = _mapHeight;

            UpdateHeightHandles();
            ConstrainHandlePositions();
            UpdateHeightData();
        }

        private void CalculateMapDimensions()
        {
            var posA = _cornerA.localPosition;
            var posB = _cornerB.localPosition;

            var minPos = new Vector3(
                Mathf.Min(posA.x, posB.x),
                0f, // Фиксированная высота для 2D карты
                Mathf.Min(posA.z, posB.z)
            );

            var maxPos = new Vector3(
                Mathf.Max(posA.x, posB.x),
                0f, // Фиксированная высота для 2D карты
                Mathf.Max(posA.z, posB.z)
            );

            var size = maxPos - minPos;

            _mapWidth = Mathf.Max(2, Mathf.RoundToInt(size.x / _cellSize) + 1);
            _mapHeight = Mathf.Max(2, Mathf.RoundToInt(size.z / _cellSize) + 1);
            _mapOrigin = minPos;
        }

        private void UpdateHeightHandles()
        {
            var requiredHandles = _mapWidth * _mapHeight;

            // Отключаем лишние хендлы
            for (var i = requiredHandles; i < _heightHandles.Count; i++)
                if (_heightHandles[i] != null)
                    _heightHandles[i].gameObject.SetActive(false);

            // Создаем недостающие хендлы
            while (_heightHandles.Count < requiredHandles)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.hideFlags = HideFlags.DontSave;
                go.name = $"HeightHandle_{_heightHandles.Count}";
                go.transform.parent = _handlesContainer;
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one * 0.3f;
                var renderer = go.GetComponent<Renderer>();
                var material = renderer.sharedMaterial;
                renderer.sharedMaterial = Instantiate(material);
                renderer.sharedMaterial.color = Color.yellow;
                renderer.receiveShadows = false;
                renderer.shadowCastingMode = ShadowCastingMode.Off;

                _heightHandles.Add(go.transform);
            }

            // Включаем и позиционируем активные хендлы
            for (var i = 0; i < requiredHandles; i++)
            {
                var handle = _heightHandles[i];
                if (handle == null)
                    continue;

                var y = handle.localPosition.y;

                if (!handle.gameObject.activeSelf)
                    y = 0;

                handle.gameObject.SetActive(true);

                var x = i % _mapWidth;
                var z = i / _mapWidth;
                var localPos = _mapOrigin + new Vector3(x * _cellSize, y, z * _cellSize);
                handle.localPosition = localPos;
            }
        }

        private void ConstrainHandlePositions()
        {
            var requiredHandles = _mapWidth * _mapHeight;
            var needsUndo = false;

            for (var i = 0; i < requiredHandles; i++)
            {
                var handle = _heightHandles[i];
                if (handle == null || !handle.gameObject.activeInHierarchy) continue;

                var x = i % _mapWidth;
                var z = i / _mapWidth;
                var targetLocalPos = _mapOrigin + new Vector3(x * _cellSize, handle.localPosition.y, z * _cellSize);

                if (handle.localPosition.x == targetLocalPos.x && handle.localPosition.z == targetLocalPos.z)
                    continue;

                if (!needsUndo)
                {
                    Undo.RecordObjects(GetActiveHandleTransforms(), "Constrain Handle Positions");
                    needsUndo = true;
                }

                handle.localPosition = targetLocalPos;
            }
        }

        private void UpdateHeightData()
        {
            var requiredHandles = _mapWidth * _mapHeight;
            var newHeightData = new byte[requiredHandles];
            var dataChanged = false;

            for (var i = 0; i < requiredHandles; i++)
            {
                var handle = _heightHandles[i];
                if (handle == null || !handle.gameObject.activeInHierarchy) continue;

                var height = math.max(0f, handle.localPosition.y - _mapOrigin.y);
                var normalizedHeight = math.clamp(height / _maxHeight, 0f, 1f);
                var byteValue = (byte)(normalizedHeight * 255f);

                newHeightData[i] = byteValue;

                if (_heightData == null || _heightData.Length <= i || _heightData[i] != byteValue)
                    dataChanged = true;
            }

            if (!dataChanged)
                return;

            Undo.RecordObject(this, "Update Height Data");
            _heightData = newHeightData;
        }

        private Transform[] GetActiveHandleTransforms()
        {
            var activeHandles = new List<Transform>();
            var requiredHandles = _mapWidth * _mapHeight;

            for (var i = 0; i < requiredHandles && i < _heightHandles.Count; i++)
            {
                var handle = _heightHandles[i];
                if (handle != null && handle.gameObject.activeInHierarchy)
                    activeHandles.Add(handle);
            }

            return activeHandles.ToArray();
        }

        [MyButton]
        public void SerializeHeightMap()
        {
            UpdateHeightHandles();
            ConstrainHandlePositions();
            UpdateHeightData();

            if (_heightData == null || _heightData.Length == 0) return;

            var heightMap = new HeightMap(_heightData, _mapWidth, _mapHeight, _maxHeight, _cellSize);
            _serializedData = heightMap.Serialize();

            Debug.Log($"HeightMap serialized: {_heightData.Length} bytes -> {_serializedData.compressedData.Length} integers");
        }

        [MyButton]
        public void DeserializeHeightMap()
        {
            if (_serializedData.compressedData == null || _serializedData.compressedData.Length == 0) return;

            var heightMap = HeightMap.Deserialize(_serializedData);

            _mapWidth = heightMap.Width;
            _mapHeight = heightMap.Height;
            _maxHeight = heightMap.MaxHeight;
            _cellSize = heightMap.CellSize;
            _heightData = heightMap.Heights;

            _lastMapWidth = _mapWidth;
            _lastMapHeight = _mapHeight;

            // Пересчитываем углы карты на основе десериализованных данных
            _mapOrigin = Vector3.zero;
            var maxPos = new Vector3((_mapWidth - 1) * _cellSize, 0f, (_mapHeight - 1) * _cellSize);

            _cornerA.localPosition = _mapOrigin;
            _cornerB.localPosition = maxPos;

            // Обновляем хендлы
            UpdateHeightHandles();

            // Восстанавливаем высоты хендлов из десериализованных данных
            var requiredHandles = _mapWidth * _mapHeight;
            for (var i = 0; i < requiredHandles && i < _heightHandles.Count; i++)
            {
                var handle = _heightHandles[i];
                if (handle == null) continue;

                var x = i % _mapWidth;
                var z = i / _mapWidth;

                // Конвертируем byte обратно в высоту
                var byteValue = _heightData[i];
                var normalizedHeight = byteValue / 255f;
                var height = normalizedHeight * _maxHeight;

                var localPos = _mapOrigin + new Vector3(x * _cellSize, height, z * _cellSize);
                handle.localPosition = localPos;
            }

            Debug.Log($"HeightMap deserialized: {_serializedData.compressedData.Length} integers -> {_heightData.Length} bytes");
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmo)
                return;

            var requiredHandles = _mapWidth * _mapHeight;
            if (_heightHandles.Count < requiredHandles) return;

            Gizmos.color = Color.green;

            // Рисуем горизонтальные линии
            for (var z = 0; z < _mapHeight; z++)
            {
                var lineStart = 0;

                while (lineStart < _mapWidth - 1)
                {
                    var startHandle = _heightHandles[z * _mapWidth + lineStart];
                    if (startHandle == null || !startHandle.gameObject.activeInHierarchy)
                    {
                        lineStart++;
                        continue;
                    }

                    var startHeight = startHandle.localPosition.y;
                    var lineEnd = lineStart;

                    // Находим конец непрерывной линии с одинаковой высотой
                    while (lineEnd < _mapWidth - 1)
                    {
                        var nextHandle = _heightHandles[z * _mapWidth + lineEnd + 1];
                        if (nextHandle == null || !nextHandle.gameObject.activeInHierarchy ||
                            Mathf.Abs(nextHandle.localPosition.y - startHeight) > 0.001f)
                            break;

                        lineEnd++;
                    }

                    // Рисуем линию от lineStart до lineEnd
                    var endHandle = _heightHandles[z * _mapWidth + lineEnd];
                    if (endHandle != null && endHandle.gameObject.activeInHierarchy)
                    {
                        Gizmos.DrawLine(
                            transform.TransformPoint(startHandle.localPosition),
                            transform.TransformPoint(endHandle.localPosition)
                        );
                    }

                    lineStart = lineEnd + 1;
                }
            }

            // Рисуем вертикальные линии
            for (var x = 0; x < _mapWidth; x++)
            {
                var lineStart = 0;

                while (lineStart < _mapHeight - 1)
                {
                    var startHandle = _heightHandles[lineStart * _mapWidth + x];
                    if (startHandle == null || !startHandle.gameObject.activeInHierarchy)
                    {
                        lineStart++;
                        continue;
                    }

                    var startHeight = startHandle.localPosition.y;
                    var lineEnd = lineStart;

                    // Находим конец непрерывной линии с одинаковой высотой
                    while (lineEnd < _mapHeight - 1)
                    {
                        var nextHandle = _heightHandles[(lineEnd + 1) * _mapWidth + x];
                        if (nextHandle == null || !nextHandle.gameObject.activeInHierarchy ||
                            Mathf.Abs(nextHandle.localPosition.y - startHeight) > 0.001f)
                            break;

                        lineEnd++;
                    }

                    // Рисуем линию от lineStart до lineEnd
                    var endHandle = _heightHandles[lineEnd * _mapWidth + x];
                    if (endHandle != null && endHandle.gameObject.activeInHierarchy)
                    {
                        Gizmos.DrawLine(
                            transform.TransformPoint(startHandle.localPosition),
                            transform.TransformPoint(endHandle.localPosition)
                        );
                    }

                    lineStart = lineEnd + 1;
                }
            }
        }
#endif
    }
}
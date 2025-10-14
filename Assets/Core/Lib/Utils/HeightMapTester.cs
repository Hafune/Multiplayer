using UnityEngine;

namespace Core.Lib
{
    public class HeightMapTester : MonoBehaviour
    {
        [SerializeField] private Transform _character;
        [SerializeField] private int _mapWidth = 10;
        [SerializeField] private int _mapHeight = 10;
        [SerializeField] private float _cellSize = 1f;
        [SerializeField] private float _heightScale = 5f;
        [SerializeField] private Material _terrainMaterial;

        private HeightMap _heightMap;
        private float[,] _heights;
        private GameObject _terrainObject;

        void Start()
        {
            GenerateRandomHeights();
            _heightMap = new HeightMap(_heights, _cellSize);
            CreateTerrainMesh();
        }

        void Update()
        {
            if (_character != null)
            {
                var position = new Vector2(_character.position.x, _character.position.z);
                var interpolatedHeight = _heightMap.GetHeightAt(position);
                _character.position = new Vector3(_character.position.x, interpolatedHeight, _character.position.z);
            }
        }

        private void GenerateRandomHeights()
        {
            _heights = new float[_mapWidth, _mapHeight];
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    _heights[x, y] = Mathf.PerlinNoise(x * 0.3f, y * 0.3f) * _heightScale;
                }
            }
        }

        private void CreateTerrainMesh()
        {
            var mesh = new Mesh();
            var vertices = new Vector3[_mapWidth * _mapHeight];
            var triangles = new int[(_mapWidth - 1) * (_mapHeight - 1) * 6];
            var uv = new Vector2[_mapWidth * _mapHeight];
            var colors = new Color[_mapWidth * _mapHeight];

            for (var y = 0; y < _mapHeight; y++)
            {
                for (var x = 0; x < _mapWidth; x++)
                {
                    var index = y * _mapWidth + x;
                    vertices[index] = new Vector3(x * _cellSize, _heights[x, y], y * _cellSize);
                    uv[index] = new Vector2((float)x / (_mapWidth - 1), (float)y / (_mapHeight - 1));

                    var heightRatio = _heights[x, y] / _heightScale;
                    colors[index] = Color.HSVToRGB(0.3f * (1f - heightRatio), 0.8f, 0.8f + 0.2f * heightRatio);
                }
            }

            var triangleIndex = 0;
            for (var y = 0; y < _mapHeight - 1; y++)
            {
                for (var x = 0; x < _mapWidth - 1; x++)
                {
                    var bottomLeft = y * _mapWidth + x;
                    var bottomRight = bottomLeft + 1;
                    var topLeft = bottomLeft + _mapWidth;
                    var topRight = topLeft + 1;

                    triangles[triangleIndex++] = bottomLeft;
                    triangles[triangleIndex++] = topLeft;
                    triangles[triangleIndex++] = bottomRight;

                    triangles[triangleIndex++] = bottomRight;
                    triangles[triangleIndex++] = topLeft;
                    triangles[triangleIndex++] = topRight;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mesh.colors = colors;
            mesh.RecalculateNormals();

            _terrainObject = new GameObject("TerrainMesh");
            var meshFilter = _terrainObject.AddComponent<MeshFilter>();
            var meshRenderer = _terrainObject.AddComponent<MeshRenderer>();

            meshFilter.mesh = mesh;
            meshRenderer.material = _terrainMaterial != null ? _terrainMaterial : CreateDefaultMaterial();
        }

        private Material CreateDefaultMaterial()
        {
            var material = new Material(Shader.Find("Standard"));
            material.color = Color.white;
            return material;
        }

        void OnDrawGizmos()
        {
            if (_heights == null) return;

            Gizmos.color = Color.yellow;
            for (var x = 0; x < _mapWidth - 1; x++)
            {
                for (var y = 0; y < _mapHeight - 1; y++)
                {
                    var p1 = new Vector3(x * _cellSize, _heights[x, y], y * _cellSize);
                    var p2 = new Vector3((x + 1) * _cellSize, _heights[x + 1, y], y * _cellSize);
                    var p3 = new Vector3(x * _cellSize, _heights[x, y + 1], (y + 1) * _cellSize);

                    Gizmos.DrawLine(p1, p2);
                    Gizmos.DrawLine(p1, p3);
                }
            }
        }
    }
}
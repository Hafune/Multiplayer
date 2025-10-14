using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class HeightMapClient : MonoConstruct
    {
        [SerializeField] private HeightMap.CompressedHeightMapData _data;
        private HeightMapService _service;
        private HeightMap _heightMap;

        private void Awake()
        {
            _heightMap = HeightMap.Deserialize(_data);
            _service = context.Resolve<HeightMapService>();
        }

        private void OnEnable() => _service.RegisterClient(this);

        private void OnDisable() => _service.RegisterClient(this);

        public Bounds GetArea()
        {
            var size = new Vector3(_data.width, _data.height, 0);
            return new Bounds(transform.position + size / 2, size);
        }

        public float GetHeight(Vector2 position) => _heightMap.GetHeightAt(position);
    }
}
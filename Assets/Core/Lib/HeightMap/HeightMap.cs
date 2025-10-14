using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Mathematics;

namespace Core.Lib
{
    public class HeightMap
    {
        private readonly byte[] _heights;
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly float _maxHeight;

        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        public float MaxHeight => _maxHeight;
        public byte[] Heights => _heights;

        public HeightMap(float[,] heights, float maxHeight, float cellSize = 1f)
        {
            _width = heights.GetLength(0);
            _height = heights.GetLength(1);
            _cellSize = cellSize;
            _maxHeight = maxHeight;

            _heights = new byte[_width * _height];
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    _heights[GetIndex(x, y)] = FloatToByte(heights[x, y]);
                }
            }
        }

        public HeightMap(byte[] heights, int width, int height, float maxHeight, float cellSize = 1f)
        {
            _heights = heights;
            _width = width;
            _height = height;
            _maxHeight = maxHeight;
            _cellSize = cellSize;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetIndex(int x, int y) => y * _width + x;

        private byte FloatToByte(float height) => (byte)(math.clamp(height / _maxHeight, 0f, 1f) * 255f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float ByteToFloat(byte height) => height / 255f * _maxHeight;

        public float GetHeightAt(Vector2 position)
        {
            var x = position.x / _cellSize;
            var y = position.y / _cellSize;

            var x0 = (int)math.floor(x);
            var y0 = (int)math.floor(y);
            var x1 = x0 + 1;
            var y1 = y0 + 1;

            x0 = math.clamp(x0, 0, _width - 1);
            y0 = math.clamp(y0, 0, _height - 1);
            x1 = math.clamp(x1, 0, _width - 1);
            y1 = math.clamp(y1, 0, _height - 1);

            var fx = x - x0;
            var fy = y - y0;

            var h00 = ByteToFloat(_heights[GetIndex(x0, y0)]);
            var h10 = ByteToFloat(_heights[GetIndex(x1, y0)]);
            var h01 = ByteToFloat(_heights[GetIndex(x0, y1)]);
            var h11 = ByteToFloat(_heights[GetIndex(x1, y1)]);

            var h0 = math.lerp(h00, h10, fx);
            var h1 = math.lerp(h01, h11, fx);

            return math.lerp(h0, h1, fy);
        }

        public CompressedHeightMapData Serialize()
        {
            return new CompressedHeightMapData
            {
                width = _width,
                height = _height,
                maxHeight = _maxHeight,
                cellSize = _cellSize,
                compressedData = CompressHeightData(_heights)
            };
        }

        public static HeightMap Deserialize(CompressedHeightMapData data)
        {
            var heights = DecompressHeightData(data.compressedData);
            return new HeightMap(heights, data.width, data.height, data.maxHeight, data.cellSize);
        }

        private static int[] CompressHeightData(byte[] data)
        {
            var compressed = new List<int>();

            for (var i = 0; i < data.Length; i++)
            {
                var currentValue = data[i];
                var runLength = 1;

                while (i + runLength < data.Length && data[i + runLength] == currentValue)
                    runLength++;

                compressed.Add(runLength);
                compressed.Add(currentValue);

                i += runLength - 1;
            }

            return compressed.ToArray();
        }

        private static byte[] DecompressHeightData(int[] compressedData)
        {
            var decompressed = new List<byte>();

            for (var i = 0; i < compressedData.Length; i += 2)
            {
                var runLength = compressedData[i];
                var value = (byte)compressedData[i + 1];

                for (var j = 0; j < runLength; j++)
                    decompressed.Add(value);
            }

            return decompressed.ToArray();
        }

        [Serializable]
        public struct CompressedHeightMapData
        {
            public int width;
            public int height;
            public float maxHeight;
            public float cellSize;
            public int[] compressedData;
        }
    }

    // public class HeightMap
    // {
    //     private readonly float[] _heights;
    //     private readonly int _width;
    //     private readonly int _height;
    //     private readonly float _cellSize;
    //
    //     public HeightMap(float[,] heights, float cellSize = 1f)
    //     {
    //         _width = heights.GetLength(0);
    //         _height = heights.GetLength(1);
    //         _cellSize = cellSize;
    //
    //         _heights = new float[_width * _height];
    //         for (var y = 0; y < _height; y++)
    //         {
    //             for (var x = 0; x < _width; x++)
    //             {
    //                 _heights[GetIndex(x, y)] = heights[x, y];
    //             }
    //         }
    //     }
    //
    //     public HeightMap(float[] heights, int width, int height, float cellSize = 1f)
    //     {
    //         _heights = heights;
    //         _width = width;
    //         _height = height;
    //         _cellSize = cellSize;
    //     }
    //
    //     private int GetIndex(int x, int y) => y * _width + x;
    //
    //     public float GetHeightAt(Vector2 position)
    //     {
    //         var x = position.x / _cellSize;
    //         var y = position.y / _cellSize;
    //
    //         var x0 = Mathf.FloorToInt(x);
    //         var y0 = Mathf.FloorToInt(y);
    //         var x1 = x0 + 1;
    //         var y1 = y0 + 1;
    //
    //         x0 = Mathf.Clamp(x0, 0, _width - 1);
    //         y0 = Mathf.Clamp(y0, 0, _height - 1);
    //         x1 = Mathf.Clamp(x1, 0, _width - 1);
    //         y1 = Mathf.Clamp(y1, 0, _height - 1);
    //
    //         var fx = x - x0;
    //         var fy = y - y0;
    //
    //         var h00 = _heights[GetIndex(x0, y0)];
    //         var h10 = _heights[GetIndex(x1, y0)];
    //         var h01 = _heights[GetIndex(x0, y1)];
    //         var h11 = _heights[GetIndex(x1, y1)];
    //
    //         var h0 = Mathf.Lerp(h00, h10, fx);
    //         var h1 = Mathf.Lerp(h01, h11, fx);
    //
    //         return Mathf.Lerp(h0, h1, fy);
    //     }
    // }
}
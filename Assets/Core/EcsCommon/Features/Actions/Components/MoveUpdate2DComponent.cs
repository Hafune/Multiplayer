using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct MoveUpdate2DComponent
    {
        [SerializeField] public bool checkWalls;
        [SerializeField] public int layer;
        [SerializeField] public float radius;
        [NonSerialized] public Vector2 lastPosition;
        [NonSerialized] public Vector2 sideMoveStartPosition;
    }
}
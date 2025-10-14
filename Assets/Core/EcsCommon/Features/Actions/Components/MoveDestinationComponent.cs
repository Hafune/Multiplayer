using UnityEngine;

namespace Core.Components
{
    public struct MoveDestinationComponent
    {
        public Vector2 position;
        public float distanceToSuccess;
        public float nextCalculateTime;
        public int lastSideDirection;
    }
}
using UnityEngine;

namespace Core.Components
{
    public struct MultiplayerDataComponent
    {
        public Quaternion rotation;
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 prediction;
        public float distance;
        public float delay;
        public MultiplayerChanges data;
    }
}
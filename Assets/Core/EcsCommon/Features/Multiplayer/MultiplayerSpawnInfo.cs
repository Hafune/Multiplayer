using System;

namespace Core
{
    [Serializable]
    public struct MultiplayerSpawnInfo
    {
        public float x;
        public float y;
        public float z;
        public float velocityX;
        public float velocityY;
        public float velocityZ;
        public int templateId;
        public string key;
    }
}
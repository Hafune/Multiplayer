using System;

namespace Core
{
    [Serializable]
    public struct MultiplayerActionInfo
    {
        public string key;
        public int state;
        public float[] values;
    }
}
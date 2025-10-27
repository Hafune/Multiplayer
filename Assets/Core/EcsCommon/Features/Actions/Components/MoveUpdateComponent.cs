using System;

namespace Core.Components
{
    [Serializable]
    public struct MoveUpdateComponent
    {
        public float forwardSpeed;
        public float sidewaysSpeed;
        public float backwardSpeed;
    }
}
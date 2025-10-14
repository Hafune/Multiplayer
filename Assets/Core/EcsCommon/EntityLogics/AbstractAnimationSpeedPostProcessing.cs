using System;
using Lib;

namespace Core.ExternalEntityLogics
{
    public abstract class AbstractAnimationSpeedPostProcessing : MonoConstruct
    {
        public abstract float CalculateValue(int entity, float speed);
        public Action<int> OnChange;
    }
}
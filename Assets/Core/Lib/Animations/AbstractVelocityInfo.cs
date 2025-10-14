using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public abstract class AbstractVelocityInfo : MonoBehaviour
    {
        public abstract Vector3 GetVelocity();

        public abstract float GetVelocity(Axis axis);
    }
}
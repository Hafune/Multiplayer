using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RotateToRandom2DLogic : AbstractEntityLogic
    {
        public override void Run(int entity) => transform.rotation = Quaternion.Euler(0, 0, 360 * Random.value);
    }
}
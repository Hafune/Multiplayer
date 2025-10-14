using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RandomRotation2DLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _transform;

        public override void Run(int entity) => _transform.rotation = Quaternion.Euler(0, 0, Random.value * 360f);
    }
}
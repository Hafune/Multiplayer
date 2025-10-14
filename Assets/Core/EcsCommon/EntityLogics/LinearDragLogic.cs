using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class LinearDragLogic : AbstractEntityLogic
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _drag;

        public override void Run(int entity){}// => _rigidbody.linlinearDamping = _drag;
    }
}
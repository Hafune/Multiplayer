using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SetParameterToZeroLogic : AbstractEntityLogic
    {
        [SerializeField] private PlayMixerTransition2DLogic _logic;

        public override void Run(int entity) => _logic.SetParameter(Vector2.zero);
    }
}
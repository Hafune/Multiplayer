using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SmoothedVector2ParameterToZeroLogic : AbstractEntityLogic
    {
        [SerializeField] private SmoothedVector2ParameterContainer _parameter;

        public override void Run(int entity) => _parameter.SmoothedParameter.TargetValue = Vector2.zero;
    }
}
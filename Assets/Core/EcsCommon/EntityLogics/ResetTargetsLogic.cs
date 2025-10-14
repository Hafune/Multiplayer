using Core.Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ResetTargetsLogic : AbstractEntityLogic
    {
        [SerializeField] private Area _area;

        public override void Run(int entity) => _area.ResetTargets();
    }
}
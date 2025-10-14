using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ActionDeathRagdollStopLogic : AbstractEntityLogic
    {
        [SerializeField] private ActionDeathRagdollLogic _ragdollLogic;

        public override void Run(int entity) => _ragdollLogic.Stop();
    }
}
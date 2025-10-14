using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class EntityLogicReference : AbstractEntityLogic
    {
        [SerializeField]private AbstractEntityLogic _logic;

        public override void Run(int entity) => _logic.Run(entity);
    }
}
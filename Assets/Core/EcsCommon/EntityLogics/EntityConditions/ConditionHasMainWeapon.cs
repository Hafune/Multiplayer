using System;
using Core.Services;

namespace Core.ExternalEntityLogics
{
    [Obsolete]
    public class ConditionHasMainWeapon : AbstractEntityCondition
    {
        // private EquipmentService _equipmentService;
        //
        // private void Awake() => _equipmentService = context.Resolve<EquipmentService>();

        public override bool Check(int entity) => false; //_equipmentService.IsWeaponEquipped;
    }
}
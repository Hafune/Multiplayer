using BansheeGz.BGDatabase;
using UnityEngine;

[BGCalcUnitDefinition("Custom/Id to hash")]
public class IdToHash : BGCalcUnit
{
    private BGCalcValueInput id;

    public override void Definition()
    {
        id = ValueInput<BGId>("Id", "id");

        ValueOutput("Hash", "b", GetValue);
    }

    private int GetValue(BGCalcFlowI flow) => Animator.StringToHash(flow.GetValue<BGId>(id).ToString());
}
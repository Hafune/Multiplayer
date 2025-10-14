using UnityEngine;

namespace Core.Lib
{
    [CreateAssetMenu(menuName = "Game Config/Progression/Balance/" + nameof(ItemMinMaxValues))]
    public class ItemMinMaxValues : AbstractMultiCurve
    {
        [field: SerializeField, CurveSetEditor]
        public AnimationCurve Min { get; set; } = AnimationCurve.Linear(0, 0, 1, 1);

        [field: SerializeField, CurveSetEditor]
        public AnimationCurve Max { get; set; } = AnimationCurve.Linear(0, 1, 1, 2);
    }
}
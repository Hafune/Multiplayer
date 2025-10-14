using UnityEngine;

namespace Core.Lib
{
    [CreateAssetMenu(menuName = "Game Config/Entities/" + nameof(ConvertToEntityReference))]
    public class ConvertToEntityReference : ScriptableObject
    {
        [field: SerializeField] public ConvertToEntity Prefab { get; private set; }
    }
}
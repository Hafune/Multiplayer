using UnityEngine;

namespace Core.Lib
{
    public class ComponentMerges : MonoBehaviour
    {
        private ComponentCombination[] _combinations;

        private void Awake() => _combinations = GetComponentsInChildren<ComponentCombination>();

        public bool TryCombine(int entity)
        {
            for (int i = 0, iMax = _combinations.Length; i < iMax; i++)
                if (_combinations[i].TryCombine(entity))
                    return true;

            return false;
        }
    }
}
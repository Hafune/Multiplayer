using UnityEngine;

namespace Core.Lib
{
    public class ComponentCombination : MonoBehaviour 
    {
        [SerializeField] private ComponentsList _has;
        [SerializeField] private ComponentsList _del;
        [SerializeField] private ComponentsList _add;

        public bool TryCombine(int entity)
        {
            if (!_has.Has(entity))
                return false;

            _del.Del(entity);
            _add.Attach(entity);

            return true;
        }
    }
}
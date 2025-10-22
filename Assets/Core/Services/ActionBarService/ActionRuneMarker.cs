using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Services
{
    public class ActionRuneMarker : MonoBehaviour
    {
        [field: SerializeField] public int RequiredLevel { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        private string key;
        public string Key => key ??= GetComponentInParent<AbstractEntityAction>().Key + "_" + name;
    }
}
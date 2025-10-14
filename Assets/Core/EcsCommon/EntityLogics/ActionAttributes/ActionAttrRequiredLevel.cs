using Core.Components;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrRequiredLevel : MonoConstruct, IActionAttr
    {
        [field: SerializeField] public int Value { get; private set; }
        
        public void Setup<T>(int entity)
        {
        }

        public void Remove<T>(int entity)
        {
        }
    }
}
using Core.Components;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class ActionAttrIcon : MonoConstruct, IActionAttr
    {
        [field: SerializeField] public Sprite Value { get; private set; }

        public void Setup<T>(int entity)
        {
        }

        public void Remove<T>(int entity)
        {
        }
    }
}
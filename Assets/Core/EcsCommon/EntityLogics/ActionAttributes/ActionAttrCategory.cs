using System;
using Core.Components;
using Core.Services;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    [Obsolete]
    public class ActionAttrCategory : MonoConstruct, IActionAttr
    {
        // [field: SerializeField] public ActionCategory Value { get; private set; }
        
        public void Setup<T>(int entity)
        {
        }

        public void Remove<T>(int entity)
        {
        }
    }
}
using System;
using UnityEngine;


namespace Lib
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TypeCheckAttribute : PropertyAttribute
    {
        public readonly Type type;

        public TypeCheckAttribute(Type type) => this.type = type;
    }
}
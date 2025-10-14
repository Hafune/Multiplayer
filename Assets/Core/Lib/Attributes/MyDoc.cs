using System;
using JetBrains.Annotations;

namespace Core.Lib
{
    public class MyDoc : Attribute
    {
        public readonly string Description;
        [CanBeNull] public readonly Type Relation;

        public MyDoc(string description, Type type = null)
        {
            Description = description;
            Relation = type;
        }
    }
}
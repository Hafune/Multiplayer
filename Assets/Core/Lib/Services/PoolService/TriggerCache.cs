using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Lib
{
    public static class TriggerCache
    {
        public static readonly Glossary<ConvertToEntity> Cache = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConvertToEntity ExtractEntity(Component col)
        {
            if (!Cache.TryGetValue(col.GetInstanceID(), out var entityRef))
                Cache.Add(col.GetInstanceID(), entityRef = col.GetComponentInParent<ConvertToEntity>());

            return entityRef;
        }
    }
}
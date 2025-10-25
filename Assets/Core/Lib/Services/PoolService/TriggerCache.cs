using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    [Obsolete("Не доделанно")]
    public static class TriggerCallbacksCache
    {
        private static Dictionary<Collider, List<MonoBehaviour>> _relations = new();
        private static Dictionary<MonoBehaviour, List<Collider>> _relations1 = new();

        public static void RegisterTrigger(MonoBehaviour owner, Collider col)
        {
            var colDispatcher = col.GetComponent<ColliderTriggerDispatcher>();
            if (!colDispatcher)
            {
                colDispatcher = col.AddComponent<ColliderTriggerDispatcher>();
                colDispatcher.hideFlags = HideFlags.DontSave;
                colDispatcher.col = col;
            }

            if (!_relations.TryGetValue(col, out var list))
                _relations[col] = list = new();

            list.Add(owner);
            
            var ownerDispatcher = owner.GetComponent<OwnerTriggerDispatcher>();
            if (!ownerDispatcher)
            {
                ownerDispatcher = owner.AddComponent<OwnerTriggerDispatcher>();
                ownerDispatcher.hideFlags = HideFlags.DontSave;
                ownerDispatcher.owner = owner;
            }

            if (!_relations1.TryGetValue(owner, out var list2))
                _relations1[owner] = list2 = new();

            list2.Add(col);
        }

        public static void UnRegisterTrigger(MonoBehaviour owner, Collider col)
        {
            var list = _relations[col];
            list.RemoveAll(p => p == owner);
        }

        private static void OnDisableCollider(Collider col)
        {
            var list = _relations[col];

            foreach (var t1 in list.ToArray())
                t1.SendMessage("OnTriggerExit", col, SendMessageOptions.DontRequireReceiver);
        }

        private static void OnDisableOwner(MonoBehaviour owner)
        {
            foreach (var (key, list) in _relations)
            foreach (var t2 in list.ToArray())
                if (t2 == owner)
                    owner.SendMessage("OnTriggerExit", key, SendMessageOptions.DontRequireReceiver);
        }

        private class ColliderTriggerDispatcher : MonoBehaviour
        {
            [NonSerialized] public Collider col;

            private void OnDisable() => OnDisableCollider(col);
        }

        private class OwnerTriggerDispatcher : MonoBehaviour
        {
            [NonSerialized] public MonoBehaviour owner;

            private void OnDisable() => OnDisableOwner(owner);
        }
    }
}
using System;
using System.Runtime.CompilerServices;
using Core.Lib.Utils;
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

    public static class TriggerDisableHandler
    {
        private static readonly Glossary<OwnerDisableHandler> _owners = new();
        private static readonly Glossary<ColliderDisableHandler> _colliders = new();

        public static void RegisterTrigger(ITriggerDispatcherTarget owner, Collider col)
        {
            if (!_owners.TryGetValue(owner.GetInstanceID(), out var ownerHandler))
            {
                _owners.Add(owner.GetInstanceID(), ownerHandler = ((Component)owner).AddComponent<OwnerDisableHandler>());
                ownerHandler.hideFlags = HideFlags.DontSave;
                ownerHandler.owner = owner;
            }

            ownerHandler.Add(col);

            if (!_colliders.TryGetValue(col.GetInstanceID(), out var colliderHandler))
            {
                _colliders.Add(col.GetInstanceID(), colliderHandler = col.AddComponent<ColliderDisableHandler>());
                colliderHandler.hideFlags = HideFlags.DontSave;
                colliderHandler.col = col;
            }

            colliderHandler.Add(owner);
        }

        public static void UnRegisterTrigger(ITriggerDispatcherTarget owner, Collider col)
        {
            ((Component)owner).GetComponent<OwnerDisableHandler>().Remove(col);
            col.GetComponent<ColliderDisableHandler>().Remove(owner);
        }

        private class ColliderDisableHandler : MonoBehaviour
        {
            [NonSerialized] public Collider col;
            private ITriggerDispatcherTarget[] list = Array.Empty<ITriggerDispatcherTarget>();
            private int count;

            public void Add(ITriggerDispatcherTarget value) => MyArrayUtility.Add(ref list, ref count, value);

            public void Remove(ITriggerDispatcherTarget value) => MyArrayUtility.Remove(ref list, ref count, value);

            private void OnDisable()
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    var behaviour = list[i];
                    behaviour.OnTriggerExit(col);
                }
            }
        }

        private class OwnerDisableHandler : MonoBehaviour
        {
            [NonSerialized] public ITriggerDispatcherTarget owner;
            private Collider[] list = Array.Empty<Collider>();
            private int count;

            public void Add(Collider value) => MyArrayUtility.Add(ref list, ref count, value);

            public void Remove(Collider value) => MyArrayUtility.Remove(ref list, ref count, value);

            private void OnDisable()
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    var col = list[i];
                    owner.OnTriggerExit(col);
                }
            }
        }
    }
}
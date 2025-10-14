using System.Runtime.CompilerServices;
using Core.Components;
using Core.Lib;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public readonly struct RelationFunctions<N, P>
        where N : struct, INodeComponent<P>
        where P : struct, IParentComponent
    {
        private readonly EcsPool<N> _nodePool;
        private readonly EcsPool<P> _parentPool;
        private readonly EcsPool<EventChildAdded> _eventChildAddedPool;

        public RelationFunctions(Context context)
        {
            var world = context.Resolve<EcsWorld>();
            _nodePool = world.GetPool<N>();
            _parentPool = world.GetPool<P>();
            _eventChildAddedPool = world.GetPool<EventChildAdded>();
        }

        public void Connect(int node, int child)
        {
            var children = _nodePool.GetOrInitialize(node).children;

            if (children.Contains(child))
                return;

            DisconnectChild(child);

            _parentPool.Add(child).entity = node;
            children.Add(child);
            _eventChildAddedPool.AddIfNotExist(node);
#if UNITY_EDITOR
            if (children.Count > 1000)
                Debug.LogWarning($"Возможна утечка памяти children.Count: {children.Count}");
#endif
        }

        public void ConnectChildsToNewParent(int node, int newNode, IEcsPool markerPool)
        {
            var fromChildren = _nodePool.Get(node).children;
            var toChildren = _nodePool.Get(newNode).children;

            for (int i = fromChildren.Count - 1; i >= 0; i--)
            {
                int child = fromChildren.Get(i);

                if (!markerPool.Has(child))
                    continue;

                fromChildren.Remove(child);
                toChildren.Add(child);
                _parentPool.Get(child).entity = newNode;
            }
        }

        public void DisconnectChild(int child)
        {
            if (!_parentPool.Has(child))
                return;

            _nodePool.Get(_parentPool.Get(child).entity).children.Remove(child);
            _parentPool.Del(child);
        }

        public void DisconnectNodeChildren(int node, IEcsPool markerPool)
        {
            if (!_nodePool.Has(node))
                return;

            var children = _nodePool.Get(node).children;

            for (int i = children.Count - 1; i >= 0; i--)
            {
                int child = children.Get(i);

                if (!markerPool.Has(child))
                    continue;

                _parentPool.Del(child);
                children.Remove(child);
            }
        }

        public void DisconnectNodeChilds(int node)
        {
            if (!_nodePool.Has(node))
                return;

            var children = _nodePool.Get(node).children;
            foreach (var child in children)
                _parentPool.Del(child);

            children.Clear();
        }

        public int GetTailEntity(int node, IEcsPool markerPool)
        {
            int tail = node;

            Repeat:
            while (_nodePool.Has(tail) && _nodePool.Get(tail).children.Count != 0)
            {
                var children = _nodePool.Get(tail).children;
#if UNITY_EDITOR
                int tailCount = 0;
                for (int i = 0, iMax = children.Count; i < iMax; i++)
                {
                    if (!markerPool.Has(children.Get(i)))
                        continue;

                    tailCount++;
                    if (tailCount == 2)
                        Debug.LogError("Есть больше одного хвоста!!");
                }
#endif
                for (int i = 0, iMax = children.Count; i < iMax; i++)
                {
                    if (!markerPool.Has(children.Get(i)))
                        continue;

                    tail = children.Get(i);
                    goto Repeat;
                }

                break;
            }

            return tail;
        }

        // public int GetChildrenCount(int node, IEcsPool markerPool)
        // {
        //     int total = 0;
        //     foreach (var child in _nodePool.Get(node).children)
        //     {
        //         if (!markerPool.Has(child))
        //             continue;
        //
        //         total++;
        //
        //         if (_nodePool.Has(child))
        //             total += GetChildrenCount(child, markerPool);
        //     }
        //
        //     return total;
        // }

        public bool TryGetSelfChild(int node, IEcsPool markerPool, out int entity)
        {
            entity = -1;

            if (!_nodePool.Has(node))
                return false;

            foreach (var child in _nodePool.Get(node).children)
            {
                if (markerPool.Has(child))
                {
                    entity = child;
                    return true;
                }
            }

            return false;
        }

        public int GetParentCount(int child, IEcsPool markerPool)
        {
            if (!_parentPool.Has(child) || !markerPool.Has(child))
                return 0;

            int total = 1;
            total += GetParentCount(_parentPool.Get(child).entity, markerPool);

            return total;
        }

        // public int GetTopParent(int entity)
        // {
        //     while (true)
        //     {
        //         if (!_parentPool.Has(entity))
        //             return entity;
        //
        //         entity = _parentPool.Get(entity).entity;
        //     }
        //
        //     return entity;
        // }

        private int GetTopParent(int entity, IEcsPool markerPool)
        {
            while (true)
            {
                if (!_parentPool.Has(entity) || !markerPool.Has(_parentPool.Get(entity).entity))
                    return entity;

                entity = _parentPool.Get(entity).entity;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator EnumerateSelfChilds(int node)
        {
            var children = _nodePool.Has(node) ? _nodePool.Get(node).children : MyList<int>.Empty;
            return new Enumerator(children);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator EnumerateSelfChilds(int node, IEcsPool markerPool)
        {
            var children = _nodePool.Has(node) ? _nodePool.Get(node).children : MyList<int>.Empty;
            return new Enumerator(children, markerPool);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator EnumerateSelfChilds(int node, IEcsPool[] markerPools)
        {
            var children = _nodePool.Has(node) ? _nodePool.Get(node).children : MyList<int>.Empty;
            return new Enumerator(children, markerPools);
        }

        public ref struct Enumerator
        {
            private readonly MyList<int> _children;
            private readonly IEcsPool _markerPool;
            private readonly IEcsPool[] _markerPools;
            private readonly byte _mode; // 0 = all, 1 = single, 2 = multi
            private int _idx;
            private readonly int _count;
            private readonly int _markerPoolsLength;

            public int Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get;
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private set;
            }

            public Enumerator(MyList<int> children)
            {
                _children = children;
                _markerPool = null;
                _markerPools = null;
                _markerPoolsLength = -1;
                _mode = 0;
                _idx = -1;
                _count = children.Count;
                Current = default;
            }

            public Enumerator(MyList<int> children, IEcsPool markerPool)
            {
                _children = children;
                _markerPool = markerPool;
                _markerPools = null;
                _markerPoolsLength = -1;
                _mode = 1;
                _idx = -1;
                _count = children.Count;
                Current = default;
            }

            public Enumerator(MyList<int> children, IEcsPool[] markerPools)
            {
                _children = children;
                _markerPool = null;
                _markerPools = markerPools;
                _markerPoolsLength = markerPools.Length;
                _mode = 2;
                _idx = -1;
                _count = children.Count;
                Current = default;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => this;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                while (++_idx < _count)
                {
                    var child = _children.Get(_idx);

                    if (_mode == 0)
                    {
                        Current = child;
                        return true;
                    }

                    if (_mode == 1)
                    {
                        if (_markerPool.Has(child))
                        {
                            Current = child;
                            return true;
                        }

                        continue;
                    }

                    bool skip = false;
                    for (var i = 0; i < _markerPoolsLength; i++)
                    {
                        var pool = _markerPools[i];
                        if (pool.Has(child))
                            continue;

                        skip = true;
                        break;
                    }

                    if (!skip)
                    {
                        Current = child;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
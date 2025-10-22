using System;
using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    public class DamageEndFrameCircleCast2DLogic : AbstractEntityLogic
    {
        [SerializeField] private DamageArea _area;
        [SerializeField] private float _damageScale;
        [SerializeField] private int _priority = -1;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private ElementalDamageContainer _elementalDamage;

        private Action<int> _dropTargets;
        private Action<int> _setTargets;
        private EcsPool<EventEndFrameCall> _endFrameCall;
        private bool _inProgress;
        private int _count;
        private int _mask;
        private readonly Collider2D[] _results = new Collider2D[128];

        private void Awake()
        {
            _mask = Physics2D.GetLayerCollisionMask(gameObject.layer);
            _endFrameCall = context.Resolve<ComponentPools>().EventEndFrameCall;
            _dropTargets = DropTargets;
            _setTargets = SetTargets;
        }

        public override void Run(int entity)
        {
            if (_inProgress)
            {
                Debug.LogError("Already in progress " + nameof(DamageEndFrameCircleCast2DLogic));
                return;
            }

            _endFrameCall.GetOrInitialize(entity).call += _setTargets;
        }

        private void SetTargets(int entity)
        {
            _count = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _results, _mask);

            if (_count == 0)
                return;

            _inProgress = true;
            var totalScale = _damageScale * _elementalDamage.GetScale();
            for (int i = 0; i < _count; i++)
                _area.TriggerEnter(_results[i], totalScale, _priority);

            _endFrameCall.GetOrInitialize(entity).call += _dropTargets;
        }

        private void DropTargets(int _)
        {
            for (int i = 0; i < _count; i++)
                _area.TriggerExit(_results[i]);

            _inProgress = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.cyan;
            Handles.DrawWireArc(transform.position, Vector3.forward, Vector3.right, 360, _radius);
        }
#endif
    }
}
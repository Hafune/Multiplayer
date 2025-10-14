using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ActionDeathRagdollLogic : AbstractEntityLogic
    {
#if UNITY_EDITOR
        [SerializeField] public GameObject _editorPelvis;
#endif
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _bodies;
        [SerializeField] private Collider[] _bodyParts;
        [SerializeField] private float _forceMultiple = 1f;
        private EcsPool<ActionDeathComponent> _actionDeathPool;
#if UNITY_EDITOR
        public void OnValidate()
        {
            _animator = _animator ? _animator : GetComponentInParent<Animator>();

            if (!_editorPelvis) 
                return;
            
            _bodies = _editorPelvis.GetComponentsInChildren<Rigidbody>();
            _bodyParts = _editorPelvis.GetComponentsInChildren<Collider>();
            _editorPelvis = null;
        }
#endif

        private void Awake()
        {
            _actionDeathPool = context.Resolve<ComponentPools>().ActionDeath;
            Stop();
        }

        public override void Run(int entity)
        {
            _animator.enabled = false;

            foreach (var body in _bodies.AsSpan())
            {
                body.isKinematic = false;
                body.interpolation = RigidbodyInterpolation.Interpolate;
                body.linearVelocity = Vector3.zero;
            }

            foreach (var col in _bodyParts.AsSpan())
                col.enabled = true;

            var actionDeath = _actionDeathPool.Get(entity);
            const float defaultMult = 500;
            _bodies[0].AddForce(
                actionDeath.impactDirection * actionDeath.impactForce * _forceMultiple * defaultMult,
                ForceMode.Impulse);
        }

        public void Stop()
        {
            foreach (var body in _bodies.AsSpan())
            {
                body.isKinematic = true;
                body.interpolation = RigidbodyInterpolation.None;
            }

            foreach (var col in _bodyParts.AsSpan())
                col.enabled = false;

            _animator.enabled = true;
        }
    }
}
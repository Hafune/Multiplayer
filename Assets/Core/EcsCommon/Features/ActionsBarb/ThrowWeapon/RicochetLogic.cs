using System;
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Lib;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.ExternalEntityLogics
{
    [Obsolete("Не закончено, требуется доработать")]
    public class RicochetLogic : AbstractEntityLogic
    {
        [SerializeField] private float _radius = 1f;
        private Action<int> _setTargets;
        private int _mask;

        private readonly Collider2D[] _results = new Collider2D[8];
        private int _ignoreEntity;
        private Rigidbody2D _body;
        private ComponentPools _pools;
        private RelationFunctions<AimComponent, TargetComponent> _relation;

        private void Awake()
        {
            _mask = Physics2D.GetLayerCollisionMask(gameObject.layer);
            _setTargets = SetTargets;
            _body = GetComponentInParent<Rigidbody2D>();
            _relation = new(context);
        }

        public override void Run(int entity) => _pools.EventEndFrameCall.GetOrInitialize(entity).call += _setTargets;

        public void SetIgnoredEntity(int entity) => _ignoreEntity = entity;

        private void SetTargets(int entity)
        {
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _results, _mask);

            if (count == 0)
            {
                _pools.EventRemoveEntity.AddIfNotExist(entity);
                return;
            }

            _results.Shuffle();
            for (int i = 0; i < count; i++)
            {
                var target = TriggerCache.ExtractEntity(_results[i]).RawEntity;
                if (target == _ignoreEntity)
                    continue;

                var point = _pools.Position.Get(entity).transform;
                var targetPosition = _pools.Position.Get(target).transform.position;
                float _angle = Vector2.SignedAngle(Vector2.right, targetPosition - point.position);
                var rotation = Quaternion.Euler(0, 0, _angle);
                point.rotation = rotation;
                _body.velocity = rotation * Vector2.right * 20;
                _relation.Connect(target, entity);
                _pools.RemoveWithTarget.Add(entity);
            }
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
using System;
using System.Collections.Generic;
using Core.Lib;
using Core.Lib.Services;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    [Obsolete("Не использовать для сущностей")]
    public class BodyProjectileLauncher2D : MonoConstruct
    {
        [SerializeField] private Rigidbody2D _prefab;
        [SerializeField] private ProjectileEmitters2D _emitters;
        [SerializeField] private float _force;

        private PrefabPool<Rigidbody2D> _projectilePool;
        private IEnumerable<Rigidbody2D> _enumerator;

        private void Awake() => _projectilePool = context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);

        public void Launch() => _emitters.ForEachEmitters(LaunchOne);

        private void LaunchOne(Transform t)
        {
            var body = _projectilePool.GetInstance(t.position, t.rotation);
            var velocity = body.transform.right * _force;
            body.velocity = velocity;
        }
    }
}
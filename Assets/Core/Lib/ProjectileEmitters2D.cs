using System;
using System.Collections.Generic;
using System.Linq;
using Core.Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ProjectileEmitters2D : AbstractProjectileEmitters
    {
        [SerializeField] private SpreadPattern[] _patterns;
        private IEnumerable<Transform> _emitters;
        private int _index;

        private void Awake() => _emitters = _patterns.SelectMany(i => i.EmittersList).Select(emitter => emitter.Point);

        public override void ForEachEmitters(Action<Transform> callback)
        {
            foreach (var t in _emitters)
                callback.Invoke(t);
        }
    }
}
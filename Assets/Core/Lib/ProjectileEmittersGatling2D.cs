using System;
using Core.Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ProjectileEmittersGatling2D : AbstractProjectileEmitters
    {
        [SerializeField] private SpreadPattern[] _patterns;
        private int _index;

        public override void ForEachEmitters(Action<Transform> callback)
        {
            foreach (var t in _patterns[_index].EmittersList)
                callback.Invoke(t.Point);

            _index = ++_index % _patterns.Length;
        }
    }
}
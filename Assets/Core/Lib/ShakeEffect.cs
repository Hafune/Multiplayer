// using Com.LuisPedroFonseca.ProCamera2D;

using System;
using UnityEngine;

namespace Core.Lib
{
    [Obsolete]
    public class ShakeEffect : AbstractEffect
    {
        // [SerializeField] private ShakePreset preset;
        // [SerializeField] private bool _playOnEnable;
        // private ProCamera2DShake _cameraShake;
        //
        // private void Awake() => _cameraShake = context.Resolve<ProCamera2D>().GetComponent<ProCamera2DShake>();
        //
        // private void OnEnable()
        // {
        //     if (_playOnEnable)
        //         Execute();
        // }
        //
        public override void Execute() {}//=> _cameraShake.Shake(preset);
    }
}
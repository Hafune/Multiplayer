using System;
// using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    [Obsolete]
    public class CameraShakeLogic : AbstractEntityLogic
    {
//         [SerializeField] private ShakePreset _preset;
//         private ProCamera2DShake _cameraShake;
//
//         private void Awake()
//         {
//             _cameraShake = context.Resolve<ProCamera2D>().GetComponent<ProCamera2DShake>();
// #if UNITY_EDITOR
//             Assert.IsNotNull(_preset);
// #endif
//         }

        public override void Run(int _) {}// _cameraShake.Shake(_preset);
    }
}
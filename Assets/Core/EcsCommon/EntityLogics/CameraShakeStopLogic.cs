// using Com.LuisPedroFonseca.ProCamera2D;

using System;

namespace Core.ExternalEntityLogics
{
    [Obsolete]
    public class CameraShakeStopLogic : AbstractEntityLogic
    {
        // private ProCamera2DShake _cameraShake;

        // private void Awake() => _cameraShake = context.Resolve<ProCamera2D>().GetComponent<ProCamera2DShake>();

        public override void Run(int _) {} //_cameraShake.StopShaking();
    }
}
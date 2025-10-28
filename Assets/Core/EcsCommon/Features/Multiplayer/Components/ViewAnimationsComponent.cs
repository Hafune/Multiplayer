using System;
using Core.ExternalEntityLogics;

namespace Core.Components
{
    [Serializable]
    public struct ViewAnimationsComponent
    {
        public PlayTransitionLogic idle;
        public PlayMixerTransition2DLogic move;
        public SmoothedVector2ParameterContainer moveDirection;
    }
}
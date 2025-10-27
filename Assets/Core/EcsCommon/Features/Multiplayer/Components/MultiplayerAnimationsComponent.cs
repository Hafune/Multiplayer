using System;
using Core.ExternalEntityLogics;

namespace Core.Components
{
    [Serializable]
    public struct MultiplayerAnimationsComponent
    {
        public PlayTransitionsLogic idle;
        public PlayMixerTransition2DLogic move;
        public SmoothedVector2ParameterContainer moveDirection;
    }
}
using System;
using Core.ExternalEntityLogics;

namespace Core.Components
{
    [Serializable]
    public struct HitImpactComponent
    {
        public AbstractEntityLogic targetEvents;
        public AbstractEntityLogic selfEvents;
    }
}
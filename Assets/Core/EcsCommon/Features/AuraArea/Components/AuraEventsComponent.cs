using System;
using Core.ExternalEntityLogics;

namespace Core.Components
{
    [Serializable]
    public struct AuraEventsComponent
    {
        public AbstractEntityLogic targetEnterEvents;
        public AbstractEntityLogic targetExitEvents;
        public AbstractEntityLogic selfEnterEvents;
        public AbstractEntityLogic selfExitEvents;
    }
}
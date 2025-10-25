using System.Collections.Generic;
using Colyseus.Schema;

namespace Core.Components
{
    public struct EventMultiplayerDataUpdated
    {
        public List<DataChange> changes;
        public float delay;
    }
}
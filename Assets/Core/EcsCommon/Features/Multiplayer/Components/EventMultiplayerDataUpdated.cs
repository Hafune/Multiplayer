using System.Collections.Generic;
using Colyseus.Schema;

namespace Core.Components
{
    public struct EventMultiplayerDataUpdated
    {
        public List<MyDataChange> changes;
        public float delay;
    }
}
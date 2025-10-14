using System;
using System.Collections.Generic;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;

namespace Core.Components
{
    [Serializable]
    public struct SlotComponent : IEcsAutoReset<SlotComponent>
    {
        public string key;
        public int maxStackCount;
        public AbstractEntityLogic vfxStartLogic;
        public AbstractEntityLogic vfxCancelLogic;
        public List<ValueData> values;
        public List<SlotTagEnum> tags;
        [NonSerialized] public int id;
        [NonSerialized] public int currentStackCount;

        public void AutoReset(ref SlotComponent c)
        {
            c.values ??= new();
            c.tags ??= new();
            c.values.Clear();
            c.tags.Clear();
            c.currentStackCount = 0;
            c.key = string.Empty;
            c.vfxStartLogic = null;
            c.vfxCancelLogic = null;
        }
    }
}
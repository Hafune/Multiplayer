using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Core.Components;
using Core.Generated;
using JetBrains.Annotations;

namespace Core.EcsCommon.ValueSlotComponents
{
    public static class SlotUtility
    {
        private static readonly Dictionary<object, ValueData[]> _cache = new();
        private static readonly Dictionary<ValueEnum, float> _tempValuesStorage = new();
        private static readonly HashSet<SlotTagEnum> _tempTagsStorage = new();
        private static readonly Dictionary<string, short> _keyIds = new();
        private static short _keyIdsCounter = 1;

        public static SlotComponent CalculateSlot(IEnumerable<IDatabaseSlotData> values)
        {
            var slot = Make();
            _tempValuesStorage.Clear();
            _tempTagsStorage.Clear();

            foreach (var model in values)
            {
                if (model == null)
                    continue;

                foreach (var data in BuildValuesByHashtable(model.Values))
                {
                    _tempValuesStorage.TryGetValue(data.valueEnum, out var currentValue);
                    _tempValuesStorage[data.valueEnum] = ValueSumRules.SumValueByRule(data.valueEnum, currentValue, data.value);
                }

                var tags = model.Tags ?? (IReadOnlyList<SlotTagEnum>)Array.Empty<SlotTagEnum>();

                for (int a = 0, aMax = tags.Count; a < aMax; a++)
                    _tempTagsStorage.Add(tags[a]);
            }

            slot.values = _tempValuesStorage
                .Select(pair => new ValueData { valueEnum = pair.Key, value = pair.Value })
                .ToList();

            slot.tags = _tempTagsStorage.ToList();

            return slot;
        }

        [Obsolete]
        public static SlotComponent CalculateSlot()//IEnumerable<ItemData_v04_07_25> values
        {
            throw new Exception();
            // var slot = Make();
            // _tempValuesStorage.Clear();
            // _tempTagsStorage.Clear();
            //
            // foreach (var itemData in values)
            // {
            //     if (itemData == null)
            //         continue;
            //
            //     foreach (var data in itemData.values)
            //     {
            //         _tempValuesStorage.TryGetValue(data.valueEnum, out var currentValue);
            //         _tempValuesStorage[data.valueEnum] = ValueSumRules.SumValueByRule(data.valueEnum, currentValue, data.value);
            //     }
            //
            //     var tags = (IReadOnlyList<SlotTagEnum>)Array.Empty<SlotTagEnum>();
            //
            //     for (int a = 0, aMax = tags.Count; a < aMax; a++)
            //         _tempTagsStorage.Add(tags[a]);
            // }
            //
            // slot.values = _tempValuesStorage
            //     .Select(pair => new ValueData { valueEnum = pair.Key, value = pair.Value })
            //     .ToList();
            //
            // slot.tags = _tempTagsStorage.ToList();
            //
            // return slot;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short GetIdByKey(string key)
        {
            if (key == string.Empty)
                return 0;

            return _keyIds.TryGetValue(key, out var id) ? id : _keyIds[key] = _keyIdsCounter++;
        }

        private static ValueData[] BuildValuesByHashtable([CanBeNull] Hashtable values)
        {
            if (values is null)
                return Array.Empty<ValueData>();

            if (_cache.TryGetValue(values, out var v))
                return v;

            var data = new ValueData[values.Count];
            int count = 0;
            foreach (DictionaryEntry entry in values)
                data[count++] = new ValueData
                {
                    valueEnum = (ValueEnum)entry.Key,
                    value = (float)entry.Value
                };

            if (_cache.Count > 100)
                _cache.Clear();

            _cache[values] = data;
            return data;
        }

        private static SlotComponent Make(List<ValueData> values = null, List<SlotTagEnum> tags = null) =>
            new()
            {
                key = string.Empty,
                values = values ?? new(),
                tags = tags ?? new(),
            };
    }
}
using System;
using Core.Lib;

namespace Core.Components
{
    [Serializable]
    public struct ConvertToEntityComponent
    {
        [ReadOnly] public ConvertToEntity convertToEntity;
    }
}
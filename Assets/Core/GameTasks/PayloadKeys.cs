using System;
using System.Collections.Generic;
using Core.Lib;
using Core.Services;
using UnityEngine;

namespace Core
{
    public sealed class PropKey<T>
    {
        internal static PropKey<T> Create() => new();
    }

    public static class CommonPayloadKeys
    {
        public static readonly PropKey<ConvertToEntity> ConvertToEntity = PropKey<ConvertToEntity>.Create();
    }
}
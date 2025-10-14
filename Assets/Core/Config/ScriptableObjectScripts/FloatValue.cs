using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/Common/" + nameof(FloatValue))]
    public class FloatValue : ScriptableObject
    {
        [field: SerializeField] public float Value { get; private set; }
    }
}
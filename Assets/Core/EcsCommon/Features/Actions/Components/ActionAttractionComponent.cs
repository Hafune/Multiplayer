using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionAttractionComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
        public Transform model;
        [NonSerialized] public Vector2 startPosition;
        [NonSerialized] public Vector2 endPosition;
        [NonSerialized] public float startTime;
    }
}
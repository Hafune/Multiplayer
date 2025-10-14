using System;
using UnityEngine;

namespace Core.Components
{
    [Tooltip("Событие обязательно делающее круг по всем системам")]
    [Serializable]
    public struct EventGlobalDispatch<T> where T : struct
    {
        
    }
}
using UnityEngine;

namespace Core.Lib
{
    public class Emitter : MonoBehaviour
    {
        [field: SerializeField] public Transform Point { get; private set; }
    }
}
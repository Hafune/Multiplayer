using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Lib
{
    public class SyncActiveStates : MonoBehaviour
    {
        [SerializeField] private GameObject[] _controlledObjects;

        private void OnValidate() => _controlledObjects = _controlledObjects?.Distinct().ToArray();

        private void OnEnable() => SetState(true);
        private void OnDisable() => SetState(false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetState(bool state)
        {
            for (int i = 0, iMax = _controlledObjects.Length; i < iMax; i++)
                _controlledObjects[i].SetActive(state);
        }
    }
}
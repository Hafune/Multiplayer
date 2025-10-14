using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    public class SyncActiveStatesSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject[] _defaults;
        [SerializeField] private GameObject[] _actives;

#if UNITY_EDITOR
        private void Awake()
        {
            for (int i = 0; i < _defaults.Length; i++)
                Assert.IsNotNull(_defaults[i], $"Element {i} in _defaults array is null");

            for (int i = 0; i < _actives.Length; i++)
                Assert.IsNotNull(_actives[i], $"Element {i} in _actives array is null");
        }
#endif
        private void OnEnable() => SetState(true);
        private void OnDisable() => SetState(false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetState(bool state)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif
            if (state)
            {
                for (int i = 0, iMax = _defaults.Length; i < iMax; i++)
                    _defaults[i].SetActive(false);

                for (int i = 0, iMax = _actives.Length; i < iMax; i++)
                    _actives[i].SetActive(true);
            }
            else
            {
                for (int i = 0, iMax = _actives.Length; i < iMax; i++)
                    _actives[i].SetActive(false);

                for (int i = 0, iMax = _defaults.Length; i < iMax; i++)
                    _defaults[i].SetActive(true);
            }
        }
    }
}
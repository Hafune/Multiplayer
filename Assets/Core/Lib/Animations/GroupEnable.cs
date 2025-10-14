using UnityEngine;

namespace Core.Lib
{
    public class GroupEnable : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _targets;

        private void OnEnable()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].enabled = true;
        }

        private void OnDisable()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].enabled = false;
        }
    }
}
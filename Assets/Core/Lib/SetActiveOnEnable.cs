using UnityEngine;

namespace Core.Lib
{
    public class SetActiveOnEnable : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _active;

        private void OnEnable()
        {
            if (_target)
                _target.SetActive(_active);
        }
    }
}
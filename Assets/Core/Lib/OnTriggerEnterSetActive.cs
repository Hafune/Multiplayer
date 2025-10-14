using UnityEngine;

namespace Core.Lib
{
    public class OnTriggerEnterSetActive : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _active;
        [SerializeField] private bool _revertOnExit;
        private int _contactCount;

#if UNITY_EDITOR
        private void OnEnable()
        {
            if (_contactCount != 0)
                Debug.LogError("Неверное количество контактов");
        }
#endif

        public void OnTriggerEnter2D(Collider2D _)
        {
            if (++_contactCount == 1)
                _target.SetActive(_active);
        }

        public void OnTriggerExit2D(Collider2D _)
        {
            if (--_contactCount == 0 && _revertOnExit)
                _target.SetActive(_active);
        }
    }
}
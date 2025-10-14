using UnityEngine;

namespace Core.Lib
{
    public class AnimateGroupFloat : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] private AbstractAnimateFloat[] _targets;

        private void Awake()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].SetValue(speed);
        }

        private void Update()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].SetValue(speed);
        }
    }
}
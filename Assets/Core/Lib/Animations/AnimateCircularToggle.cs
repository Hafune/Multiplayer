using UnityEngine;

namespace Core.Lib
{
    public class AnimateCircularToggle : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] public float toggleTime;
        [SerializeField] private AbstractAnimateToggle[] _targets;

        private int _lastIndex = -1;
        private float _time;
        private readonly MyList<Data> _active = new();

        private void OnDisable()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].SetValue(false);

            _active.Clear();
            _lastIndex = -1;
            _time = 0f;
        }

        private void Update()
        {
            int targetIndex = (int)(_time += Time.deltaTime * speed) % _targets.Length;
            float time = Time.time;

            if (_lastIndex != targetIndex || _active.Count == _targets.Length)
            {
                _targets[targetIndex].SetValue(true);
                _active.Add(new Data { index = targetIndex, timer = time + toggleTime });
                _lastIndex = targetIndex;
            }

            for (int i = _active.Count - 1; i >= 0; i--)
            {
                var data = _active.Get(i);
                
                if (data.timer > time)
                    continue;

                _active.RemoveAt(i);
                _targets[data.index].SetValue(false);
            }
        }

        private struct Data
        {
            public int index;
            public float timer;
        }
    }
}
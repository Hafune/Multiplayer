using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class TimeStepLogic : AbstractEntityLogic, IActionSubStartLogic
    {
        [SerializeField] [Min(0)] private float _timeStep;
        [SerializeField] private AbstractEntityLogic _next;
        private float _startTime;

#if UNITY_EDITOR
        private void Awake() => Assert.IsNotNull(_next);
#endif

        public void SubStart(int entity) => _startTime = Time.time;

        public override void Run(int entity)
        {
            if (Time.time - _startTime <= _timeStep)
                return;

            _startTime += _timeStep;
            _next.Run(entity);
        }
    }
}
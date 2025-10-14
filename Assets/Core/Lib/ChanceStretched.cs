using Random = UnityEngine.Random;

namespace Core.Lib
{
    public class ChanceStretched
    {
        private readonly float _stepsToComplete;
        private readonly float _minSteps;
        private readonly float _maxSteps;
        private readonly float _chance;
        private float _roll;

        public ChanceStretched(float chance)
        {
            _stepsToComplete = (1 - chance) / chance;
            const float window = .5f;
            _minSteps = _stepsToComplete * window;
            _maxSteps = _stepsToComplete + _minSteps;
            _chance = chance;
            _roll = Random.value * _stepsToComplete;
        }

        public bool TryTrigger()
        {
            _roll++;
            
            if (_roll < _minSteps)
                return false;

            if (_roll > _maxSteps)
            {
                _roll -= _stepsToComplete;
                return true;
            }

            var success = _chance >= Random.value;
            
            if (success)
                _roll -= _stepsToComplete;
            
            return success;
        }
    }
}
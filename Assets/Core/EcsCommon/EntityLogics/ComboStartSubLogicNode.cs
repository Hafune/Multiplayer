using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ComboStartSubLogicNode : MonoConstruct, IActionSubStartLogic, IActionSubCancelLogic
    {
        private const float _maxStepDelay = 0.5f;
        private AbstractEntityLogic[] _logics;
        private int _index;
        private float _lastTime;

        private void Awake() => _logics = transform.GetSelfChildrenComponents<AbstractEntityLogic>();

        public void SubStart(int entity)
        {
            _index = Time.time - _lastTime < _maxStepDelay ? (_index + 1) % _logics.Length : 0; 
            _logics[_index].Run(entity);
        }

        public void SubCancel(int entity) => _lastTime = Time.time;
    }
}
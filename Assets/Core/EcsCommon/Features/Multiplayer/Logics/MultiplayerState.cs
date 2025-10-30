using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class MultiplayerState : MonoConstruct
    {
        [SerializeField] private MultiplayerSendStateLogic _idle;
        [SerializeField] private MultiplayerSendStateLogic _move;
        [SerializeField] private SmoothedVector2ParameterContainer _moveDirection;

        private readonly List<MultiplayerSendStateLogic> _totalActions = new();
        private int _applyIndex;
        private int _sendIndex;
        private bool _wasIdle = false;
        private bool _wasRun = false;
        private Vector3 _lastVelocity;

        private void Awake()
        {
            GetComponentsInChildren(_totalActions);
            _applyIndex = _totalActions.IndexOf(_idle);
            _sendIndex = _applyIndex;
        }

        public void SetParameters(int i, Vector3 forwardVelocity)
        {
            _lastVelocity = forwardVelocity.normalized;
            _moveDirection.SmoothedParameter.TargetValue = new(_lastVelocity.x, _lastVelocity.z);

            if (_totalActions[_applyIndex] != _idle && _totalActions[_applyIndex] != _move)
                return;

            if (_lastVelocity == Vector3.zero)
            {
                if (!_wasIdle)
                    _idle.RunMultiplayerLogic(i);
            }
            else
            {
                if (!_wasRun)
                    _move.RunMultiplayerLogic(i);
            }
        }

        public void WriteState(MultiplayerSendStateLogic multiplayerSendLogic) => _sendIndex = _totalActions.IndexOf(multiplayerSendLogic);
        public short GetState() => (short)_sendIndex;

        public void RunMultiplayerLogic(int entity, int index)
        {
            _applyIndex = index;
            if (_totalActions[_applyIndex] == _idle || _totalActions[_applyIndex] == _move)
                SetParameters(entity, _lastVelocity);
            else
                _totalActions[_applyIndex].RunMultiplayerLogic(entity);
        }
    }
}
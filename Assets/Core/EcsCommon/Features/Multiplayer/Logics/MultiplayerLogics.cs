using System;
using System.Collections.Generic;
using Core.Generated;
using Lib;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class MultiplayerLogics : MonoConstruct
    {
        [SerializeField] private MultiplayerSendLogic _idle;
        [SerializeField] private MultiplayerSendLogic _move;
        [SerializeField] private SmoothedVector2ParameterContainer _moveDirection;

        private readonly List<MultiplayerSendLogic> _totalActions = new();
        private int _applyIndex;
        private int _sendIndex;
        private Action<int> _endFrameCall;
        private ComponentPools _pools;
        private bool _wasIdle = false;
        private bool _wasRun = false;
        private Vector3 _lastVelocity;

        private void Awake()
        {
            GetComponentsInChildren(_totalActions);
            _endFrameCall = EndFrameCall;
            _pools = context.Resolve<ComponentPools>();
            _applyIndex = _totalActions.IndexOf(_idle);
        }

        public void SetParameters(int i, Vector3 forwardVelocity)
        {
            _lastVelocity = forwardVelocity;
            _moveDirection.SmoothedParameter.TargetValue = new(forwardVelocity.x, forwardVelocity.z);

            if (_totalActions[_applyIndex] != _idle && _totalActions[_applyIndex] != _move)
                return;

            if (forwardVelocity == Vector3.zero)
            {
                if (!_wasIdle)
                    _idle.RunMultiplayerLogic(i);
            }
            else
            {
                if (!_wasRun)
                    _move.RunMultiplayerLogic(i);

                forwardVelocity.Normalize();
            }
        }

        public void SendData(int entity, MultiplayerSendLogic multiplayerSendLogic)
        {
            _sendIndex = _totalActions.IndexOf(multiplayerSendLogic);
            _pools.EventEndFrameCall.GetOrInitialize(entity).call += _endFrameCall;
        }

        public void RunMultiplayerLogic(int entity, int index)
        {
            _applyIndex = index;
            if (_totalActions[_applyIndex] == _idle || _totalActions[_applyIndex] == _move)
                SetParameters(entity, _lastVelocity);
            else
                _totalActions[_applyIndex].RunMultiplayerLogic(entity);
        }

        private void EndFrameCall(int entity) => MultiplayerManager.Instance.SendData("logics", JsonUtility.ToJson(new MultiplayerActionInfo
        {
            key = _pools.MultiplayerData.Get(entity).data.SessionId,
            index = _sendIndex
        }));
    }
}
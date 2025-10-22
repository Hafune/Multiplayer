using System;
using System.Collections.Generic;
using Colyseus;
using Core.Generated;
using Core.Lib;
using Core.Lib.Services;
using Lib;
using Reflex;
using Unity.Cinemachine;
using UnityEngine;

namespace Core
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        [SerializeField] private MultiplayerView[] _views;
        [SerializeField] private AnimationClip[] _states;
        [SerializeField] private ConvertToEntity _player;
        [SerializeField] private ConvertToEntity _enemy;
        private ColyseusRoom<State> _room;
        private Context _context;
        private ComponentPools _componentPools;
        private readonly Dictionary<string, ConvertToEntity> _storages = new();
        private MyPool _pool;
        private readonly Dictionary<AnimationClip, int> _stateIds = new();

        protected override void Start()
        {
            base.Start();

            int index = 0;
            foreach (var clip in _states)
                _stateIds[clip] = index++;

            Instance.InitializeClient();
            Connect();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.visible = !hasFocus;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
            _context.Resolve<CinemachineCamera>().GetComponent<CinemachineInputAxisController>().enabled = hasFocus;
        }

        public void SetupContext(Context context)
        {
            _context = context;
            _componentPools = context.Resolve<ComponentPools>();
            _pool = context.Resolve<PoolService>().DontDisposablePool;
        }

        public string GetClientId() => _room.Id;

        public (Dictionary<AnimationClip, int>, AnimationClip[]) GetStates() => (_stateIds, _states);

        public void SendData(string key, Dictionary<string, object> data) => _room.Send(key, data);

        public void SendMessage(string key, string data) => _room.Send(key, data);

        private async void Connect()
        {
            _room = await Instance.client.JoinOrCreate<State>("state_handler");
            _room.OnStateChange += OnChange;
            _room.OnMessage<string>("shoot", data =>
            {
                var info = JsonUtility.FromJson<SpawnInfo>(data);

                foreach (var set in _views)
                {
                    if (set.key.TemplateId != info.templateId)
                        continue;

                    var position = new Vector3(info.x, info.y, info.z);
                    var velocity = new Vector3(info.velocityX, info.velocityY, info.velocityZ);
                    var convertToEntity = _pool.GetInstanceByPrefab(set.view, position, Quaternion.identity);
                    var body = convertToEntity.GetComponent<Rigidbody>();
                    body.linearVelocity = velocity;

                    if (velocity == Vector3.zero)
                        return;

                    var forward = velocity.normalized;
                    var up = Math.Abs(forward.y) > 0.99f ? new Vector3(0, 0, 1) : new Vector3(0, 1, 0);
                    var rotation = Quaternion.LookRotation(forward, up);
                    body.rotation = rotation;
                    // var multiplayerData = convertToEntity.GetComponent<MultiplayerData>();
                    // multiplayerData.SetupData(data);
                    // _pools.MultiplayerData.Add(convertToEntity.RawEntity).data = multiplayerData;
                    // _storages[key] = convertToEntity;

                    return;
                }
            });
        }

        private void OnChange(State state, bool isFirstState)
        {
            if (!isFirstState)
                return;

            state.players.ForEach(AddPlayer);
            _room.State.players.OnAdd += AddPlayer;
            _room.State.players.OnRemove += RemovePlayer;
        }

        private void AddPlayer(string key, Player data)
        {
            var position = new Vector3(data.x, 0, data.z);
            var prefab = key == _room.SessionId ? _player : _enemy;

            var convertToEntity = _context.Instantiate(prefab, position, Quaternion.identity);
            var multiplayerData = convertToEntity.GetComponent<MultiplayerData>();
            multiplayerData.SetupData(data);
            _componentPools.MultiplayerData.Add(convertToEntity.RawEntity).data = multiplayerData;
            _storages[key] = convertToEntity;

            if (key != _room.SessionId)
                return;

            var camera = _context.Resolve<CinemachineCamera>();
            camera.Follow = convertToEntity.transform;
            var follow = camera.GetComponent<CinemachineOrbitalFollow>();
            follow.HorizontalAxis.Value = 0;
            follow.VerticalAxis.Value = 20;
            follow.RadialAxis.Value = follow.RadialAxis.Range.y;
        }

        private void RemovePlayer(string key, Player data)
        {
            var entity = _storages[key].RawEntity;
            _componentPools.EventRemoveEntity.AddIfNotExist(entity);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _room.Leave();
        }

        [Serializable]
        private struct MultiplayerView
        {
            public ConvertToEntity key;
            public ConvertToEntity view;
        }
    }
}
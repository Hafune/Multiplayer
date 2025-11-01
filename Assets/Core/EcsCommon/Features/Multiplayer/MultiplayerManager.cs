using System;
using System.Collections.Generic;
using Colyseus;
using Colyseus.Schema;
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Lib.Services;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using Unity.Cinemachine;
using UnityEngine;

namespace Core
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        [SerializeField] private MultiplayerView[] _views;
        [SerializeField] private ConvertToEntity _player;
        [SerializeField] private ConvertToEntity _enemy;
        private ColyseusRoom<State> _room;
        private Context _context;
        private ComponentPools _pools;
        private MyPool _objectPool;
        private EcsFilter _othersFilter;
        private EcsFilter _playerFilter;
        private readonly Dictionary<string, ConvertToEntity> _multiplayerEntities = new();
        private readonly List<DataChange> _immediatelyChanges = new();
        private readonly List<MultiplayerDamageAreaDataComponent> _damages = new();

        protected override void Start()
        {
            base.Start();
            _immediatelyChanges.Add(new DataChange { Field = nameof(Player.x) });
            _immediatelyChanges.Add(new DataChange { Field = nameof(Player.y) });
            _immediatelyChanges.Add(new DataChange { Field = nameof(Player.z) });
            _immediatelyChanges.Add(new DataChange { Field = nameof(Player.velocityX) });
            _immediatelyChanges.Add(new DataChange { Field = nameof(Player.velocityY) });
            _immediatelyChanges.Add(new DataChange { Field = nameof(Player.velocityZ) });
            _immediatelyChanges.Add(new DataChange { Field = nameof(Player.bodyAngle) });

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
            _pools = context.Resolve<ComponentPools>();
            _objectPool = context.Resolve<PoolService>().DontDisposablePool;
            var world = context.Resolve<EcsWorld>();
            _othersFilter = world.Filter<MultiplayerDataComponent>().Inc<MultiplayerStateComponent>().Exc<Player1UniqueTag>().End();
            _playerFilter = world.Filter<Player1UniqueTag>().End();
        }

        public string GetClientId() => _room.Id;

        public void SendData(string key, Dictionary<string, object> data) => _room.Send(key, data);

        public void SendData(string key, string data) => _room.Send(key, data);

        public void PrepareToSendDamage(MultiplayerDamageAreaDataComponent data) => _damages.Add(data);

        public void SendAll(MultiplayerMoveData data)
        {
            _room.Send("move", data);

            if (_damages.Count == 0)
                return;

            _room.Send("damage", _damages);
            _damages.Clear();
        }

        private async void Connect()
        {
            var options = new Dictionary<string, object>
            {
                { "hp", 100 }
            };

            _room = await Instance.client.JoinOrCreate<State>("state_handler", options);
            _room.OnStateChange += OnChange;
            _room.OnMessage<string>("shoot", data =>
            {
                var info = JsonUtility.FromJson<MultiplayerSpawnInfo>(data);

                foreach (var set in _views)
                {
                    if (set.key.TemplateId != info.templateId)
                        continue;

                    var position = new Vector3(info.x, info.y, info.z);
                    var velocity = new Vector3(info.velocityX, info.velocityY, info.velocityZ);
                    var convertToEntity = _objectPool.GetInstanceByPrefab(set.view, position, Quaternion.identity);
                    var body = convertToEntity.GetComponent<Rigidbody>();
                    body.linearVelocity = velocity;

                    if (velocity == Vector3.zero)
                        return;

                    var forward = velocity.normalized;
                    var up = Math.Abs(forward.y) > 0.99f ? new Vector3(0, 0, 1) : new Vector3(0, 1, 0);
                    var rotation = Quaternion.LookRotation(forward, up);
                    body.rotation = rotation;

                    return;
                }
            });

            _room.OnMessage<string>("state", data =>
            {
                var info = JsonUtility.FromJson<MultiplayerActionInfo>(data);

                foreach (var i in _othersFilter)
                {
                    if (info.key != _pools.MultiplayerData.Get(i).data.SessionId)
                        continue;

                    _immediatelyChanges[0].Value = info.values[0];
                    _immediatelyChanges[0].PreviousValue = info.values[0];
                    _immediatelyChanges[1].Value = info.values[1];
                    _immediatelyChanges[2].Value = info.values[2];
                    _immediatelyChanges[3].Value = info.values[3];
                    _immediatelyChanges[4].Value = info.values[4];
                    _immediatelyChanges[5].Value = info.values[5];
                    _immediatelyChanges[6].Value = info.values[6];

                    _pools.MultiplayerData.Get(i).data.OnChange(_immediatelyChanges);
                    _pools.MultiplayerState.Get(i).state.RunMultiplayerLogic(i, info.state);
                }
            });

            _room.OnMessage<string>("damage", data =>
            {
                var damageData = JsonUtility.FromJson<MultiplayerDamageAreaDataComponent>(data);
                
                if (damageData.impactIndex == -1)
                    return;
                
                int entity = _playerFilter.GetFirst();
                _pools.EventKnockdown.AddIfNotExist(entity);
            });
        }

        private void OnChange(State state, bool isFirstState)
        {
            if (!isFirstState)
                return;

            state.players.ForEach(AddPlayer);
            _room.State.players.OnAdd += AddPlayer;
            _room.State.players.OnRemove += RemovePlayer;
            _room.OnStateChange -= OnChange;
        }

        private void AddPlayer(string key, Player data)
        {
            if (!Application.isPlaying)
                return;

            var position = new Vector3(data.x, data.y, data.z);
            var prefab = key == _room.SessionId ? _player : _enemy;

            var convertToEntity = _context.Instantiate(prefab, position, Quaternion.identity);
            var multiplayerData = convertToEntity.GetComponent<MultiplayerChanges>();
            multiplayerData.SetupData(key, data);
            ref var dataComponent = ref _pools.MultiplayerData.Add(convertToEntity.RawEntity);
            dataComponent.data = multiplayerData;
            dataComponent.position = position;
            _multiplayerEntities[key] = convertToEntity;

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
            var entity = _multiplayerEntities[key].RawEntity;
            _pools.EventRemoveEntity.AddIfNotExist(entity);
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
using System.Collections.Generic;
using Colyseus;
using Core.Generated;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

namespace Core
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        [SerializeField] private ConvertToEntity _player;
        [SerializeField] private ConvertToEntity _enemy;
        private ColyseusRoom<State> _room;
        private Context _context;
        private ComponentPools _pools;
        private readonly Dictionary<string, ConvertToEntity> _storages = new();

        public void SetupContext(Context context)
        {
            _context = context;
            _pools = context.Resolve<ComponentPools>();
        }

        protected override void Start()
        {
            base.Start();
            Instance.InitializeClient();
            Connect();
        }

        private async void Connect()
        {
            _room = await Instance.client.JoinOrCreate<State>("state_handler");
            _room.OnStateChange += OnChange;
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
            _pools.MultiplayerData.Add(convertToEntity.RawEntity).data = multiplayerData;
            _storages[key] = convertToEntity;
        }

        private void RemovePlayer(string key, Player data)
        {
            var entity = _storages[key].RawEntity;
            _pools.EventRemoveEntity.AddIfNotExist(entity);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _room.Leave();
        }

        public void SendData(string key, Dictionary<string, object> data) => _room.Send(key, data);
    }
}
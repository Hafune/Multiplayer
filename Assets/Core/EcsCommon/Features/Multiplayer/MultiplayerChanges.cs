using System.Collections.Generic;
using Colyseus.Schema;
using Core.Generated;
using Core.Lib;
using Lib;

namespace Core
{
    public class MultiplayerChanges : MonoConstruct
    {
        private ComponentPools _pools;
        private ConvertToEntity _convertToEntity;
        private int _index;

        public Player Player { get; private set; }
        public string SessionId { get; private set; }
        private readonly List<MyDataChange> _changes = new();

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _convertToEntity = GetComponent<ConvertToEntity>();
            _convertToEntity.BeforeEntityDeleted += OnRemove;
        }

        public void SetupData(string sessionId, Player data)
        {
            Player = data;
            Player.OnChange += OnChange;
            SessionId = sessionId;
        }

        public void OnChange(List<DataChange> changes)
        {
            var entity = _convertToEntity.RawEntity;
            if (entity == -1)
                return;

            _changes.Clear();
            foreach (var dataChange in changes)
                _changes.Add(new MyDataChange { Field = dataChange.Field, Value = dataChange.Value });

            ref var update = ref _pools.EventMultiplayerDataUpdated.GetOrInitialize(entity);
            update.changes = _changes;
            update.delay = Player.patchRate / 1000f;
        }

        private void OnRemove(ConvertToEntity _)
        {
            if (Player != null)
                Player.OnChange -= OnChange;

            Player = null;
        }
    }

    public struct MyDataChange
    {
        public string Field;
        public object Value;
    }
}
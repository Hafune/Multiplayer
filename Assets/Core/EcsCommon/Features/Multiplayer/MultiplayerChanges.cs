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

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _convertToEntity = GetComponent<ConvertToEntity>();
            _convertToEntity.BeforeEntityDeleted += OnRemove;
        }

        public void SetupData(Player data)
        {
            Player = data;
            Player.OnChange += OnChange;
        }

        private void OnRemove(ConvertToEntity _)
        {
            if (Player != null)
                Player.OnChange -= OnChange;

            Player = null;
        }

        private void OnChange(List<DataChange> changes)
        {
            var entity = _convertToEntity.RawEntity;
            if (entity == -1)
                return;
            
            ref var update = ref _pools.EventMultiplayerDataUpdated.GetOrInitialize(entity);
            update.changes = changes;
            update.delay = Player.patchRate / 1000f;
        }
    }
}
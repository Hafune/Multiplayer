using System.Collections.Generic;
using Colyseus.Schema;
using Core.Generated;
using Core.Lib;
using Lib;

namespace Core
{
    public class MultiplayerChanges : MonoConstruct
    {
        private Player _data;
        private ComponentPools _pools;
        private ConvertToEntity _convertToEntity;
        private int _index;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _convertToEntity = GetComponent<ConvertToEntity>();
            _convertToEntity.BeforeEntityDeleted += OnRemove;
        }

        public void SetupData(Player data)
        {
            _data = data;
            _data.OnChange += OnChange;
        }

        private void OnRemove(ConvertToEntity _)
        {
            if (_data != null)
                _data.OnChange -= OnChange;

            _data = null;
        }

        private void OnChange(List<DataChange> changes)
        {
            var entity = _convertToEntity.RawEntity;
            if (entity == -1)
                return;
            
            ref var update = ref _pools.EventMultiplayerDataUpdated.GetOrInitialize(entity);
            update.changes = changes;
            update.delay = _data.patchRate / 1000f;
        }
    }
}
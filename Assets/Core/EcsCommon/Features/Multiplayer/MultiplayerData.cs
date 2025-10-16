using System.Collections.Generic;
using Colyseus.Schema;
using Core.Generated;
using Core.Lib;
using Lib;

namespace Core
{
    public class MultiplayerData : MonoConstruct
    {
        private Player _data;
        private ComponentPools _pools;
        private ConvertToEntity _convertToEntity;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _convertToEntity = GetComponent<ConvertToEntity>();
        }

        public void SetupData(Player data) => data.OnChange += OnChange;

        public void OnDisable()
        {
            if (_data != null)
                _data.OnChange -= OnChange;
        }

        private void OnChange(List<DataChange> changes)
        {
            var entity = _convertToEntity.RawEntity;
            if (entity != -1)
                _pools.EventMultiplayerDataUpdated.GetOrInitialize(entity).changes = changes;
        }
    }
}
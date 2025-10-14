using System.Linq;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class EntityActionDynamic : AbstractEntityActionStateful
    {
         private AbstractEntityActionStateful[] _logics;
        private GameObject[] _objects;
        private AbstractEntityActionStateful _activeLogic;
        private GameObject _activeObject;

        private void Awake()
        {
            _logics = GetComponentsInChildren<AbstractEntityActionStateful>(true)
                .Where(i => !ReferenceEquals(i, this)).ToArray();

            _objects = _logics.Select(i => i.gameObject).ToArray();
        }

        public override bool CheckConditionLogic(int entity)
        {
            if (_activeObject is not null && _activeObject.activeSelf)
                return _activeLogic is not null && _activeLogic.CheckConditionLogic(entity);

            _activeLogic = null;

            for (int i = 0, iMax = _objects.Length; i < iMax; i++)
                if (_objects[i].activeSelf)
                {
                    _activeLogic = _logics[i];
                    _activeObject = _objects[i];
                    break;
                }

            return _activeLogic is not null && _activeLogic.CheckConditionLogic(entity);
        }

        public override void StartLogic(int entity) => _activeLogic?.StartLogic(entity);

        public override void UpdateLogic(int entity) => _activeLogic?.UpdateLogic(entity);

        public override void CompleteStreamingLogic(int entity) => _activeLogic?.CompleteStreamingLogic(entity);

        public override void CancelLogic(int entity) => _activeLogic?.CancelLogic(entity);
    }
}
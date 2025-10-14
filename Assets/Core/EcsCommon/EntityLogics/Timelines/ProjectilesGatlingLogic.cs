using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ProjectilesGatlingLogic : AbstractEntityLogic, IActionSubStartLogic
    {
        [SerializeField] private ProjectileLaunch2DWithEmittersLogic[] _projectileLaunchers;
        private ushort _index;

        public void SubStart(int entity) => _index = 0;

        public override void Run(int entity) => _projectileLaunchers[_index++ % _projectileLaunchers.Length].Run(entity);
    }
}
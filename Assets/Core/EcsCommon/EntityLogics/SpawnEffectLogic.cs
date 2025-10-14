using Core.Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SpawnEffectLogic : AbstractEntityLogic
    {
        [SerializeField] private AbstractEffect _spawnEffect;

        public override void Run(int entity) => _spawnEffect.Execute();
    }
}
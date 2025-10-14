using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Lib
{
    public class AudioSourceStopLogic : AbstractEntityLogic
    {
        [SerializeField] private AbstractAudioSourceClient _target;

        public override void Run(int entity) => _target.Stop();
    }
}
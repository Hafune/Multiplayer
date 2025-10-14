using Core.Lib;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class NextIfHitFirstFrameLogic : AbstractEntityLogic
    {
        [SerializeField] private DamageArea _area;
        private AbstractEntityLogic[] _logics;

        private void Awake() => _logics = transform.GetSelfChildrenComponents<AbstractEntityLogic>();

        public override void Run(int i)
        {
            if (_area.ReceiversClearCount == 0)
                for (int a = 0, aMax = _logics.Length; a < aMax; a++)
                    _logics[a].Run(i);
        }
    }
}
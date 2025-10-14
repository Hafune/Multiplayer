using Core.Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class ChanceLogic : AbstractEntityLogic 
    {
        [SerializeField] private AbstractEntityLogic _next;
        [SerializeField] private float _chance;
        private ChanceStretched _chanceStretched;

        private void Awake()
        {
            _chanceStretched = new ChanceStretched(_chance);
            Assert.IsNotNull(_next);
        }

        public override void Run(int i)
        {
            if (_chanceStretched.TryTrigger())
                _next.Run(i);
        }
    }
}
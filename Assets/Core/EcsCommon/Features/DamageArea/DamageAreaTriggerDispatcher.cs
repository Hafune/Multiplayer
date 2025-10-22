using JetBrains.Annotations;
using UnityEngine;

namespace Core.Lib
{
    public class DamageAreaTriggerDispatcher : MonoBehaviour, ITriggerDispatcherTarget2D
    {
        [SerializeField] private DamageArea _target;
        [SerializeField] private float _damageScale = 1;
        [SerializeField] private int _priority = -1;
        [SerializeField] [CanBeNull] private ElementalDamageContainer _elementalDamage;

        public void OnTriggerEnter2D(Collider2D col) =>
            _target.TriggerEnter(col, _damageScale * (_elementalDamage?.GetScale() ?? 1), _priority);

        public void OnTriggerExit2D(Collider2D col) => _target.TriggerExit(col);
    }
}
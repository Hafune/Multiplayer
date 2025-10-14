using System;
using Core.Components;
using Core.Lib;
using DamageNumbersPro;
using UnityEngine;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/Effects/" + nameof(CommonValues))]
    public class CommonValues : ScriptableObject
    {
        [field: SerializeField] public DamageNumber DamageTextEffectPrefab { get; private set; }
        [field: SerializeField] public DamageNumber CriticalDamageTextEffectPrefab { get; private set; }
        [field: SerializeField] public Material ChampionMaterial { get; private set; }
        [field: SerializeField] public Material EliteMaterial { get; private set; }
        [field: SerializeField] public Material MinionMaterial { get; private set; }
        [field: SerializeField] public ConvertToEntity FrozenOrbAffixPrefab { get; private set; }
        [field: SerializeField] public ConvertToEntity ArcaneOrbAffixPrefab { get; private set; }
        [field: SerializeField] public ConvertToEntity PoisonPuddleAffixPrefab { get; private set; }
        [field: SerializeField] public SlotComponent StoneSkinAffixSlot { get; private set; }
        [field: SerializeField] public SlotComponent FastAffixSlot { get; private set; }
    }
}
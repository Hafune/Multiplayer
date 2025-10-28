using Core.Components;
using Core.Generated;
using Core.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class InitSubComponentSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventWaitInit>> _hasInitFilter;

        private readonly EcsFilterInject<Inc<EventInit, MultiplayerDataComponent, BaseValueComponent<HitPointMaxValueComponent>>>
            _multiplayerFilter;

        private readonly EcsFilterInject<Inc<EventInit, EnemyComponent>> _enemyFilter;
        private readonly EcsFilterInject<Inc<EventInit, Player1UniqueTag>> _player1Filter;
        private readonly EcsFilterInject<Inc<EventInit, ActionMoveComponent>> _actionMoveFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            if (_hasInitFilter.Value.GetEntitiesCount() == 0)
                return;

            foreach (var i in _hasInitFilter.Value)
            {
                _pools.EventWaitInit.Get(i).convertToEntity.ApplyCache();
                _pools.EventWaitInit.Del(i);
                _pools.EventInit.Add(i);

                _pools.ActionCurrent.Add(i);
                _pools.ActionComplete.Add(i);
                _pools.BaseSlowdownMoveValue.Add(i);
                _pools.BaseSlowdownAnimationValue.Add(i);
                _pools.BaseVulnerabilityCriticalChanceValue.Add(i);

                _pools.HitAdditionalCriticalChance.Add(i);
                _pools.SlotTimersCooldown.Add(i);
            }

            foreach (var i in _multiplayerFilter.Value)
                _pools.BaseHitPointMaxValue.Get(i).baseValue = _pools.MultiplayerData.Get(i).data.Player.maxHp;

            foreach (var i in _actionMoveFilter.Value)
            {
                _pools.MoveDirection.Add(i);
                _pools.MoveDestination.Add(i);
                _pools.MoveDesiredPosition.Add(i);
            }

            //засунуть в плеерс сервис
            foreach (var i in _player1Filter.Value)
            {
                _pools.DropTargetWhenSheDeath.Add(i);
                _pools.PlayerInputMemory.Add(i);
                _pools.Player1Controller.Add(i);
                _pools.ActionAttack.Add(i);
                _pools.ActionAttackInstant.Add(i);

                ref var healingPotionCooldown = ref _pools.CooldownValueHealingPotionValue.Add(i);
                healingPotionCooldown.startTime = float.MinValue;
                healingPotionCooldown.value = 30f;

                // _experienceService.SetupExperienceValueAndLevelSlot(i);

                _pools.BaseActionCostPerSecondValue.AddIfNotExist(i);
                _pools.BaseAddScoreOnDeathValue.AddIfNotExist(i);
                _pools.BaseArmorValue.AddIfNotExist(i);
                _pools.BaseArmorPropertyValue.AddIfNotExist(i);
                _pools.BaseArmorPercentValue.AddIfNotExist(i);
                _pools.BaseAttackSpeedPercentValue.AddIfNotExist(i);
                _pools.BaseAttackSpeedValue.AddIfNotExist(i);
                _pools.BaseBlockAmountMaxValue.AddIfNotExist(i);
                _pools.BaseBlockAmountMinValue.AddIfNotExist(i);
                _pools.BaseBlockChanceValue.AddIfNotExist(i);
                _pools.BaseCriticalChanceValue.AddIfNotExist(i);
                _pools.BaseCriticalDamageValue.AddIfNotExist(i);
                _pools.BaseDamageMaxValue.AddIfNotExist(i);
                _pools.BaseDamageMinValue.AddIfNotExist(i);
                _pools.BaseDamagePropertyMaxValue.AddIfNotExist(i);
                _pools.BaseDamagePropertyMinValue.AddIfNotExist(i);
                _pools.BaseDamagePercentValue.AddIfNotExist(i);
                _pools.BaseDamageReflectionPercentValue.AddIfNotExist(i);
                _pools.BaseDamageValue.AddIfNotExist(i);
                _pools.BaseDexterityValue.AddIfNotExist(i);
                _pools.BaseEnergyMaxValue.AddIfNotExist(i);
                _pools.BaseEnergyValue.AddIfNotExist(i);
                _pools.BaseEvasionValue.AddIfNotExist(i);
                _pools.BaseExperienceValue.AddIfNotExist(i);
                _pools.BaseExplosionScaleValue.AddIfNotExist(i);
                _pools.BaseExtraGoldWhenKillingValue.AddIfNotExist(i);
                _pools.BaseHealingPerHitValue.AddIfNotExist(i);
                _pools.BaseHealingPercentPerSecondValue.AddIfNotExist(i);
                _pools.BaseHealingPerSecondValue.AddIfNotExist(i);
                _pools.BaseHitPointMaxValue.AddIfNotExist(i);
                _pools.BaseHitPointPercentValue.AddIfNotExist(i);
                _pools.BaseIntelligenceValue.AddIfNotExist(i);
                _pools.BaseManaPointMaxValue.AddIfNotExist(i);
                _pools.BaseMoveSpeedPercentValue.AddIfNotExist(i);
                _pools.BaseMoveSpeedValue.AddIfNotExist(i);
                _pools.BaseRecoveryTimeReductionValue.AddIfNotExist(i);
                _pools.BaseResistanceAllValue.AddIfNotExist(i);
                _pools.BaseResourceCostsReductionValue.AddIfNotExist(i);
                _pools.BaseStrengthValue.AddIfNotExist(i);
                _pools.BaseVitalityPercentValue.AddIfNotExist(i);
                _pools.BaseVitalityValue.AddIfNotExist(i);
                _pools.BaseIncomingDamagePercentValue.Add(i);

                _pools.BaseDamagePhysicalValue.Add(i);
                _pools.BaseDamageColdValue.Add(i);
                _pools.BaseDamageFireValue.Add(i);
                _pools.BaseDamageLightningValue.Add(i);

                _pools.BaseBarbDamageBashValue.Add(i);
                _pools.BaseBarbDamageCleaveValue.Add(i);
                _pools.BaseBarbDamageFrenzyValue.Add(i);
                _pools.BaseBarbDamageHammerOfTheAncientsValue.Add(i);
                _pools.BaseBarbDamageOverPowerValue.Add(i);
                _pools.BaseBarbDamageRendValue.Add(i);
                _pools.BaseBarbDamageRevengeValue.Add(i);
                _pools.BaseBarbDamageSeismicSlamValue.Add(i);
                _pools.BaseBarbDamageWeaponThrowValue.Add(i);
                _pools.BaseBarbDamageWhirlwindValue.Add(i);

                _pools.BaseBarbFrenzyStackValue.Add(i);
            }

            foreach (var i in _enemyFilter.Value)
            {
                _pools.DropTargetWhenSheDeath.Add(i);
                _pools.BaseIncomingDamagePercentValue.Add(i);
                _pools.BaseHitPointPercentValue.AddIfNotExist(i);
                _pools.BaseAttackSpeedPercentValue.AddIfNotExist(i);
                _pools.BaseMoveSpeedPercentValue.AddIfNotExist(i);
            }
        }
    }
}
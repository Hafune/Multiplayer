using System;
using Core.Components;
using UnityEngine.UIElements;

namespace Core.Views
{
    public class LowHitPointsView : AbstractUIDocumentView
    {
        private LowHitPointsVT _root;
        private float _hp;
        private float _maxHp;

        protected override void OnAwake()
        {
            DisplayFlex();
            _root = new LowHitPointsVT(RootVisualElement);
            _root.frame.RegisterCallback<TransitionEndEvent>(_ => SwitchStyle());

            var worldMessages = context.Resolve<EcsEngine>().UiEntityFactory;

            worldMessages.BuildUiEntityWithLink<Player1UniqueTag>()
                .ValueUpdated<HitPointValueComponent>((i, v) =>
                {
                    _hp = v.value;
                    RefreshFrame();
                })
                .ValueUpdated<HitPointMaxValueComponent>((i,v) =>
                {
                    _maxHp = v.value;
                    RefreshFrame();
                });
        }

        private void RefreshFrame()
        {
            float percent = Math.Max(_hp, 0f) / Math.Max(_maxHp, .2f);

            if (_maxHp == 0)
                percent = 1f;

            const float lowLevel = .35f;
            if (percent > lowLevel)
            {
                _root.frame.RemoveFromClassList(LowHitPointsVT.s_low_hp_fade_in_full);
                _root.frame.RemoveFromClassList(LowHitPointsVT.s_low_hp_fade_in_half);
                _root.frame.AddToClassList(LowHitPointsVT.s_low_hp_fade_out);
                return;
            }

            if (percent <= lowLevel &&
                !_root.frame.ClassListContains(LowHitPointsVT.s_low_hp_fade_in_full) &&
                !_root.frame.ClassListContains(LowHitPointsVT.s_low_hp_fade_in_half))
            {
                _root.frame.AddToClassList(LowHitPointsVT.s_low_hp_fade_in_full);
                _root.frame.RemoveFromClassList(LowHitPointsVT.s_low_hp_fade_out);
            }
        }

        private void SwitchStyle()
        {
            if (_root.frame.ClassListContains(LowHitPointsVT.s_low_hp_fade_in_full))
            {
                _root.frame.RemoveFromClassList(LowHitPointsVT.s_low_hp_fade_in_full);
                _root.frame.AddToClassList(LowHitPointsVT.s_low_hp_fade_in_half);
            }
            else if (_root.frame.ClassListContains(LowHitPointsVT.s_low_hp_fade_in_half))
            {
                _root.frame.RemoveFromClassList(LowHitPointsVT.s_low_hp_fade_in_half);
                _root.frame.AddToClassList(LowHitPointsVT.s_low_hp_fade_in_full);
            }
        }
    }
}
using Core.Components;
using Core.Generated;
using UnityEngine.UIElements;

namespace Core.Views
{
    public class HitPointView : AbstractUIDocumentView
    {
        private ProgressBar _hpBar;
        private ProgressBar _mpBar;
        private HitPointVT _root;
        private ComponentPools _pools;

        protected override void OnAwake()
        {
            _pools = context.Resolve<ComponentPools>();
            _root = new HitPointVT(RootVisualElement);
            _hpBar = _root.hpBar;
            _mpBar = _root.resourceBar;

            context
                .Resolve<EcsEngine>().UiEntityFactory
                .BuildUiEntityWithLink<Player1UniqueTag>()
                .ValueUpdated<HitPointMaxValueComponent>((i, _) => RefreshHpBar(i))
                .ValueUpdated<HitPointValueComponent>((i, _) => RefreshHpBar(i))
                .ValueUpdated<ManaPointMaxValueComponent>((i, _) => RefreshMpBar(i))
                .ValueUpdated<ManaPointValueComponent>((i, _) => RefreshMpBar(i));
            
            DisplayFlex();
        }

        private void RefreshHpBar(int i)
        {
            _hpBar.value = _pools.HitPointValue.Get(i).value;
            _hpBar.highValue = _pools.HitPointMaxValue.Get(i).value;
            _hpBar.title = $"{_hpBar.value}/{_hpBar.highValue}";
        }

        private void RefreshMpBar(int i)
        {
            _mpBar.value = _pools.ManaPointValue.Get(i).value;
            _mpBar.highValue = _pools.ManaPointMaxValue.Get(i).value;
            _mpBar.title = $"{_mpBar.value}/{_mpBar.highValue}";
        }
    }
}
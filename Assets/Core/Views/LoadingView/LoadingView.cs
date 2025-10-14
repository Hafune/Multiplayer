using Core.Services;
using Lib;
using UnityEngine.UIElements;

namespace Core.Views
{
    public class LoadingView : AbstractUIDocumentView
    {
        private LoadingVT _root;

        protected override void OnAwake()
        {
            _root = new LoadingVT(RootVisualElement);
            // new UIElementLocalization(RootVisualElement, I18N.LoadingVT.Table);

            DisplayFlex();
            _root.loadingContainer.DisplayNone();
            _root.spinner.DisplayNone();
            _root.save.DisplayNone();

            var addressableService = context.Resolve<AddressableService>();
            addressableService.OnNextSceneWillBeLoad += () =>
            {
                _root.loadingContainer.DisplayFlex();
                _root.loadingBar.title = string.Empty;
                _root.loadingBar.value = 0;
            };
            addressableService.OnSceneLoaded += _root.loadingContainer.DisplayNone;
            addressableService.OnLoadingPercentChange += value =>
            {
                _root.loadingBar.value = value;
                // _root.loadingBar.title = ConvertBytesToMegabytes(addressableService.TotalLoadingValue * value) + " MB";
                _root.loadingBar.title = FormatFloatToStringUtility.ToPercentInt(value) + "%";
            };

            var playerDataService = context.Resolve<PlayerDataService>();

            void HideAfterTransition(TransitionEndEvent _)
            {
                _root.save.UnregisterCallback<TransitionEndEvent>(HideAfterTransition);
                _root.save.RemoveFromClassList(LoadingVT.s_savedHide);
                _root.save.DisplayNone();
            }

            playerDataService.OnSaveEnd += () =>
            {
                if (!_root.save.IsDisplayFlex())
                    return;
                
                _root.save.AddToClassList(LoadingVT.s_savedHide);
                _root.save.RegisterCallback<TransitionEndEvent>(HideAfterTransition);
            };
        }

        private static string ConvertBytesToMegabytes(float bytes) => (bytes / 1024f / 1024f).ToString("0.00");
    }
}
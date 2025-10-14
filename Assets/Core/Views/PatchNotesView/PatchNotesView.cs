using Core.Lib;
using Core.Services;
using Core.Views;
using I2.Loc;
using Lib;
using UnityEngine.UIElements;

namespace Core
{
    public class PatchNotesView : AbstractUIDocumentView
    {
        private GameplayStateService _gameplayStateService;

        protected override void OnAwake()
        {
            _gameplayStateService = context.Resolve<GameplayStateService>();
            _gameplayStateService.OnShowHud += DisplayFlex;
            _gameplayStateService.OnHideHud += DisplayNone;

            var _root = new PatchNotesVT(RootVisualElement);
            var patchNotesService = context.Resolve<PatchNotesService>();
            patchNotesService.OnChange += () =>
            {
                _root.versionText.text = patchNotesService.GetVersionText();
                
                if (!patchNotesService.IsNew())
                    return;

                _root.showButton.RemoveFromClassList(PatchNotesVT.s_patch_notes_hide);
            };

            new UIElementL2Localization(RootVisualElement, nameof(ScriptLocalization.PatchNotesVT));
            _root.patchContainer.DisplayNone();
            var timeScaleService = context.Resolve<TimeScaleService>();
            
            _root.showButton.RegisterCallback<ClickEvent>(_ =>
            {
                timeScaleService.Pause();
                _root.patchContainer.DisplayFlex();
                _root.showButton.AddToClassList(PatchNotesVT.s_patch_notes_hide);
                patchNotesService.MarkAsRead();
            });
            _root.closeButton.RegisterCallback<ClickEvent>(_ =>
            {
                timeScaleService.Resume();
                _root.patchContainer.DisplayNone();
            });

            RegisterLockGrounds(_root.showButton);
        }

        private void RegisterLockGrounds(VisualElement ele)
        {
            ele.RegisterPointerEnterLeaveEvents(
                () => _gameplayStateService.LockGround(ele),
                () => _gameplayStateService.UnlockGround(ele));
        }
    }
}
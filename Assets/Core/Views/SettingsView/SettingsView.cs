using System;
using Core.Services;
using Core.Views;
using I2.Loc;
using Lib;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class SettingsView : AbstractUIDocumentView
    {
        private SettingsVT _root;
        private readonly float _step = .04f;
        private UiFocusableService _focusableService;
        private GameplayStateService _gameplayStateService;

        protected override void OnAwake()
        {
            var audioSourceService = context.Resolve<AudioSourceService>();
            _focusableService = context.Resolve<UiFocusableService>();
            _gameplayStateService = context.Resolve<GameplayStateService>();
            _gameplayStateService.OnHideAllMenus += () =>
            {
                if (RootVisualElement.IsDisplayFlex())
                    Hide();
            };
            
            _root = new SettingsVT(RootVisualElement);
            new UIElementL2Localization(RootVisualElement, nameof(ScriptLocalization.SettingsVT));

            SetupSlider(
                _root.masterVolume,
                change: audioSourceService.SetMasterVolume,
                getValue: () => audioSourceService.MasterVolume,
                onChange: null);

            SetupSlider(
                _root.backgroundVolume,
                change: audioSourceService.SetMusicVolume,
                getValue: () => audioSourceService.MusicVolume,
                onChange: null);

            var uiSfx = context.Resolve<UiSfxTemplate>();
            var submitSourcePrefab = uiSfx.SubmitSourceContainer.GetComponent<AudioSource>();
            float time = Time.time;
            float playDelay = .1f;

            SetupSlider(
                _root.sfxVolume,
                change: audioSourceService.SetSFXVolume,
                getValue: () => audioSourceService.SFXVolume,
                onChange: () =>
                {
                    if (Time.time - time < playDelay)
                        return;

                    time = Time.time;
                    audioSourceService.PlayOneShotUI(submitSourcePrefab);
                });

            var triggerSourcePrefab = uiSfx.TriggerSourceContainer.GetComponent<AudioSource>();
            var selectSourcePrefab = uiSfx.SelectSourceContainer.GetComponent<AudioSource>();
            var localizationService = context.Resolve<LocalizationService>();

            _root.language.RegisterCallback<FocusEvent>(_ => audioSourceService.PlayOneShotUI(selectSourcePrefab));
            _root.language.RegisterCallback<NavigationMoveEvent>(evt =>
            {
                switch (evt.direction)
                {
                    case NavigationMoveEvent.Direction.Left:
                        audioSourceService.PlayOneShotUI(triggerSourcePrefab);
                        localizationService.SelectPrevious();
                        evt.StopPropagation();
                        evt.PreventDefault();
                        break;
                    case NavigationMoveEvent.Direction.Right:
                        audioSourceService.PlayOneShotUI(triggerSourcePrefab);
                        localizationService.SelectNext();
                        evt.StopPropagation();
                        evt.PreventDefault();
                        break;
                }
            });

            _root.languageLeftArrow.pickingMode = PickingMode.Position;
            _root.languageLeftArrow.RegisterCallback<ClickEvent>(_ => localizationService.SelectPrevious());

            _root.languageRightArrow.pickingMode = PickingMode.Position;
            _root.languageRightArrow.RegisterCallback<ClickEvent>(_ => localizationService.SelectNext());

            audioSourceService.OnReload += () =>
            {
                _root.masterVolume.SetValueWithoutNotify(audioSourceService.MasterVolume);
                _root.backgroundVolume.SetValueWithoutNotify(audioSourceService.MusicVolume);
                _root.sfxVolume.SetValueWithoutNotify(audioSourceService.SFXVolume);
            };

            var settingsService = context.Resolve<SettingsService>();
            settingsService.OnToggleView += () =>
            {
                if (RootVisualElement.IsDisplayFlex())
                    Hide();
                else
                    Show();
            };
            
            _root.closeButton.RegisterCallback<ClickEvent>(_ => settingsService.ToggleView());
        }

        private void Show()
        {
            _gameplayStateService.HideAllMenus();
            _gameplayStateService.LockGround(this);
            RootVisualElement.DisplayFlex();
            _root.masterVolume.Focus();
            _focusableService.AddLayer(_root.focusableNode);
        }

        private void Hide()
        {
            _gameplayStateService.UnlockGround(this);
            _focusableService.RemoveLayer();
            RootVisualElement.DisplayNone();
        }

        private void SetupSlider(Slider slider, Action<float> change, Func<float> getValue, Action onChange)
        {
            //костыль что бы событие RegisterValueChangedCallback не перекрывало изменения навигации
            //внутри слайдера знаение шага захардкожено
            bool navigationIsActive = false;
            slider.AddManipulator(new SliderSfxManipulator(context));
            slider.RegisterCallback<NavigationMoveEvent>(evt =>
            {
                switch (evt.direction)
                {
                    case NavigationMoveEvent.Direction.Left:
                        navigationIsActive = true;
                        change(getValue() - _step);
                        evt.PreventDefault();
                        evt.StopPropagation();
                        break;
                    case NavigationMoveEvent.Direction.Right:
                        navigationIsActive = true;
                        change(getValue() + _step);
                        evt.PreventDefault();
                        evt.StopPropagation();
                        break;
                }
            });

            slider.RegisterValueChangedCallback(evt =>
            {
                if (!navigationIsActive)
                    change(evt.newValue);

                navigationIsActive = false;
                onChange?.Invoke();
            });
        }
    }
}
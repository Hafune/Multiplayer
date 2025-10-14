// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class SettingsVT
{
    public const string s_focusableLabel = "focusable-label";
    public const string s_fontSize = "font-size";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_settingsContainer = "settings-container";
    public const string s_slider = "slider";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_unityBaseField__input = "unity-base-field__input";
    
    public Core.FocusableNodeElement focusableNode;
    public Core.FocusableSlider masterVolume;
    public Core.FocusableSlider backgroundVolume;
    public Core.FocusableSlider sfxVolume;
    public Core.FocusableVisualElement language;
    public VisualElement languageLeftArrow;
    public VisualElement languageRightArrow;
    public VisualElement closeButton;
    
    public SettingsVT(VisualElement root)
    {
        focusableNode = root.Q<Core.FocusableNodeElement>("FocusableNode");
        masterVolume = root.Q<Core.FocusableSlider>("MasterVolume");
        backgroundVolume = root.Q<Core.FocusableSlider>("BackgroundVolume");
        sfxVolume = root.Q<Core.FocusableSlider>("SfxVolume");
        language = root.Q<Core.FocusableVisualElement>("Language");
        languageLeftArrow = root.Q<VisualElement>("LanguageLeftArrow");
        languageRightArrow = root.Q<VisualElement>("LanguageRightArrow");
        closeButton = root.Q<VisualElement>("CloseButton");
    }
}

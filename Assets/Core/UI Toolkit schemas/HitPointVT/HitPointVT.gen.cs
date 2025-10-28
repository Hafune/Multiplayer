// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class HitPointVT
{
    public const string s_bar = "bar";
    public const string s_container = "container";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_resource_bar = "resource_bar";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_unityLabel = "unity-label";
    public const string s_unityProgressBar__background = "unity-progress-bar__background";
    public const string s_unityProgressBar__container = "unity-progress-bar__container";
    public const string s_unityProgressBar__progress = "unity-progress-bar__progress";
    public const string s_unityProgressBar__titleContainer = "unity-progress-bar__title-container";
    
    public VisualElement barArea;
    public ProgressBar hpBar;
    public ProgressBar resourceBar;
    public Label level;
    
    public HitPointVT(VisualElement root)
    {
        barArea = root.Q<VisualElement>("BarArea");
        hpBar = root.Q<ProgressBar>("HpBar");
        resourceBar = root.Q<ProgressBar>("ResourceBar");
        level = root.Q<Label>("Level");
    }
}

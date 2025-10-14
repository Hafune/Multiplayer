// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class TutorialFocusVT
{
    public const string s_fade = "fade";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_textSize = "text-size";
    public const string s_tint = "tint";
    public const string s_tintShadow = "tint-shadow";
    public const string s_title_size = "title_size";
    public const string s_transition = "transition";
    
    public VisualElement top;
    public VisualElement left;
    public VisualElement center;
    public VisualElement right;
    public VisualElement bottom;
    public VisualElement container;
    public Label label;
    
    public TutorialFocusVT(VisualElement root)
    {
        top = root.Q<VisualElement>("Top");
        left = root.Q<VisualElement>("Left");
        center = root.Q<VisualElement>("Center");
        right = root.Q<VisualElement>("Right");
        bottom = root.Q<VisualElement>("Bottom");
        container = root.Q<VisualElement>("Container");
        label = root.Q<Label>("Label");
    }
}

// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class PossiblePropertyVT
{
    public const string s_blue_rarity_title_color = "blue_rarity_title_color";
    public const string s_default_text_size = "default_text_size";
    public const string s_double_dots = "double_dots";
    public const string s_gray_rarity_title_color = "gray_rarity_title_color";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_possiblePropertiesValue = "possible-properties-value";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_required_level_red = "required_level_red";
    public const string s_secondary_labels_color = "secondary_labels_color";
    public const string s_size_1x1 = "size_1x1";
    public const string s_size_1x2 = "size_1x2";
    public const string s_system_labels = "system_labels";
    public const string s_system_labels_color = "system_labels_color";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_value_labels_color = "value_labels_color";
    public const string s_yellow_rarity_title_color = "yellow_rarity_title_color";
    
    public VisualElement container;
    public Label valueMin;
    public Label valueMax;
    public Label description;
    
    public PossiblePropertyVT(VisualElement root)
    {
        container = root.Q<VisualElement>("Container");
        valueMin = root.Q<Label>("ValueMin");
        valueMax = root.Q<Label>("ValueMax");
        description = root.Q<Label>("Description");
    }
}

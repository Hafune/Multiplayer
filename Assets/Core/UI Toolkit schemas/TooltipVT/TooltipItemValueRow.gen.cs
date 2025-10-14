// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class TooltipItemValueRow
{
    public const string s_blue_rarity_title_color = "blue_rarity_title_color";
    public const string s_default_text_size = "default_text_size";
    public const string s_double_dots = "double_dots";
    public const string s_gray_rarity_title_color = "gray_rarity_title_color";
    public const string s_required_level_red = "required_level_red";
    public const string s_secondary_labels_color = "secondary_labels_color";
    public const string s_size_1x1 = "size_1x1";
    public const string s_size_1x2 = "size_1x2";
    public const string s_system_labels = "system_labels";
    public const string s_system_labels_color = "system_labels_color";
    public const string s_value_labels_color = "value_labels_color";
    public const string s_yellow_rarity_title_color = "yellow_rarity_title_color";
    
    public VisualElement defaultValueIcon;
    public VisualElement replacedValueIcon;
    public Label value;
    public Label title;
    
    public TooltipItemValueRow(VisualElement root)
    {
        defaultValueIcon = root.Q<VisualElement>("DefaultValueIcon");
        replacedValueIcon = root.Q<VisualElement>("ReplacedValueIcon");
        value = root.Q<Label>("Value");
        title = root.Q<Label>("Title");
    }
}

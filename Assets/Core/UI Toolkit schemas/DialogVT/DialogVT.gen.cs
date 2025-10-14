// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class DialogVT
{
    public const string s_close_icon_button = "close_icon_button";
    public const string s_icon = "icon";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textContainer = "text-container";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_titleUnderline = "title-underline";
    public const string s_window = "window";
    
    public VisualElement background;
    public VisualElement icon;
    public Label title;
    public Label message;
    public VisualElement rewardsContainer;
    public ScrollView choices;
    public VisualElement closeIconButton;
    
    public DialogVT(VisualElement root)
    {
        background = root.Q<VisualElement>("Background");
        icon = root.Q<VisualElement>("Icon");
        title = root.Q<Label>("Title");
        message = root.Q<Label>("Message");
        rewardsContainer = root.Q<VisualElement>("RewardsContainer");
        choices = root.Q<ScrollView>("Choices");
        closeIconButton = root.Q<VisualElement>("CloseIconButton");
    }
}

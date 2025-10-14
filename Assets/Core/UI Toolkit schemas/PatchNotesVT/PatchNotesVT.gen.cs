// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class PatchNotesVT
{
    public const string s_default_text_size = "default_text_size";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_patch_notes_hide = "patch_notes_hide";
    public const string s_path_container = "path_container";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    
    public VisualElement showButton;
    public Label versionText;
    public VisualElement patchContainer;
    public Label patchText;
    public VisualElement closeButton;
    
    public PatchNotesVT(VisualElement root)
    {
        showButton = root.Q<VisualElement>("ShowButton");
        versionText = root.Q<Label>("VersionText");
        patchContainer = root.Q<VisualElement>("PatchContainer");
        patchText = root.Q<Label>("PatchText");
        closeButton = root.Q<VisualElement>("CloseButton");
    }
}

// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class LoadingVT
{
    public const string s_container = "container";
    public const string s_itemProgressBar = "item-progress-bar";
    public const string s_loadingContainer = "loading-container";
    public const string s_loadingLabel = "loading-label";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_saved = "saved";
    public const string s_savedHide = "saved-hide";
    public const string s_spinner = "spinner";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_unityLabel = "unity-label";
    public const string s_unityProgressBar__background = "unity-progress-bar__background";
    public const string s_unityProgressBar__container = "unity-progress-bar__container";
    public const string s_unityProgressBar__progress = "unity-progress-bar__progress";
    
    public VisualElement loadingContainer;
    public ProgressBar loadingBar;
    public VisualElement spinner;
    public Label save;
    
    public LoadingVT(VisualElement root)
    {
        loadingContainer = root.Q<VisualElement>("LoadingContainer");
        loadingBar = root.Q<ProgressBar>("LoadingBar");
        spinner = root.Q<VisualElement>("Spinner");
        save = root.Q<Label>("Save");
    }
}

// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class SelectCharacterVT
{
    public const string s_characterFrame__container = "character-frame__container";
    public const string s_characterFrame__selection = "character-frame__selection";
    public const string s_container = "container";
    public const string s_focusableLabel = "focusable-label";
    public const string s_main_value_size = "main_value_size";
    public const string s_mainMenuItem = "main-menu-item";
    public const string s_navigationBar = "navigation-bar";
    public const string s_navigationBarBackground = "navigation-bar-background";
    public const string s_navigationBarButtonContainer = "navigation-bar-button-container";
    public const string s_navigationBarButtonIcon = "navigation-bar-button-icon";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_titleBg = "title-bg";
    
    public VisualElement charactersMenu;
    public VisualElement characterMenuSlots;
    public VisualElement firstSlot;
    public VisualElement selectionFrame;
    public Label ok;
    public VisualElement navigationBar;
    
    public SelectCharacterVT(VisualElement root)
    {
        charactersMenu = root.Q<VisualElement>("CharactersMenu");
        characterMenuSlots = root.Q<VisualElement>("CharacterMenuSlots");
        firstSlot = root.Q<VisualElement>("FirstSlot");
        selectionFrame = root.Q<VisualElement>("SelectionFrame");
        ok = root.Q<Label>("Ok");
        navigationBar = root.Q<VisualElement>("NavigationBar");
    }
}

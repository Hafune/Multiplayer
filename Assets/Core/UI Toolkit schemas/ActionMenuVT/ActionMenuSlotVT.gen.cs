// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class ActionMenuSlotVT
{
    public const string s_actionSlotRuneName = "action-slot-rune-name";
    public const string s_auxiliary = "auxiliary";
    public const string s_closeButton = "close-button";
    public const string s_closeButtonBottom = "close-button-bottom";
    public const string s_closeIcon = "close-icon";
    public const string s_content = "content";
    public const string s_defensive = "defensive";
    public const string s_header = "header";
    public const string s_headerTitle = "header-title";
    public const string s_keyBinding = "key-binding";
    public const string s_main_value_size = "main_value_size";
    public const string s_mouseIcon = "mouse-icon";
    public const string s_offensive = "offensive";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_passiveIcon = "passive-icon";
    public const string s_passiveIconContainer = "passive-icon-container";
    public const string s_passiveName = "passive-name";
    public const string s_passiveSkillsRow = "passive-skills-row";
    public const string s_passiveSlot = "passive-slot";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_section = "section";
    public const string s_sectionTitle = "section-title";
    public const string s_skillBinding = "skill-binding";
    public const string s_skillCategory = "skill-category";
    public const string s_skillFrame = "skill-frame";
    public const string s_skillIcon = "skill-icon";
    public const string s_skillIconContainer = "skill-icon-container";
    public const string s_skillName = "skill-name";
    public const string s_skillsGrid = "skills-grid";
    public const string s_skillSlot = "skill-slot";
    public const string s_skillsRow = "skills-row";
    public const string s_skillsWindow = "skills-window";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    
    public VisualElement container;
    public Label categoryTitle;
    public VisualElement icon;
    public Label actionName;
    public Label runeName;
    public VisualElement hotKeyContainer;
    public VisualElement hotkeyIcon;
    public Label hotkey;
    public VisualElement unactiveOverlay;
    public TemplateContainer labelNewVT;
    
    public ActionMenuSlotVT(VisualElement root)
    {
        container = root.Q<VisualElement>("Container");
        categoryTitle = root.Q<Label>("CategoryTitle");
        icon = root.Q<VisualElement>("Icon");
        actionName = root.Q<Label>("ActionName");
        runeName = root.Q<Label>("RuneName");
        hotKeyContainer = root.Q<VisualElement>("HotKeyContainer");
        hotkeyIcon = root.Q<VisualElement>("HotkeyIcon");
        hotkey = root.Q<Label>("Hotkey");
        unactiveOverlay = root.Q<VisualElement>("UnactiveOverlay");
        labelNewVT = root.Q<TemplateContainer>("LabelNewVT");
    }
}

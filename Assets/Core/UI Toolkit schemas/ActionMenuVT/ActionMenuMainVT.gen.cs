// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class ActionMenuMainVT
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
    public const string s_mouseIcon = "mouse-icon";
    public const string s_offensive = "offensive";
    public const string s_passiveIcon = "passive-icon";
    public const string s_passiveIconContainer = "passive-icon-container";
    public const string s_passiveName = "passive-name";
    public const string s_passiveSkillsRow = "passive-skills-row";
    public const string s_passiveSlot = "passive-slot";
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
    
    public Button closeButton;
    public TemplateContainer actionSlotMouseLeft;
    public TemplateContainer actionSlotMouseRight;
    public TemplateContainer actionSlotButton1;
    public TemplateContainer actionSlotButton2;
    public TemplateContainer actionSlotButton3;
    public TemplateContainer actionSlotButton4;
    public VisualElement passiveSlot1;
    public VisualElement passiveIcon1;
    public Label passiveName1;
    public VisualElement passiveSlot2;
    public VisualElement passiveIcon2;
    public Label passiveName2;
    public VisualElement passiveSlot3;
    public VisualElement passiveIcon3;
    public Label passiveName3;
    public VisualElement passiveSlot4;
    public VisualElement passiveIcon4;
    public Label passiveName4;
    public Button closeButtonBottom;
    
    public ActionMenuMainVT(VisualElement root)
    {
        closeButton = root.Q<Button>("close-button");
        actionSlotMouseLeft = root.Q<TemplateContainer>("ActionSlotMouseLeft");
        actionSlotMouseRight = root.Q<TemplateContainer>("ActionSlotMouseRight");
        actionSlotButton1 = root.Q<TemplateContainer>("ActionSlotButton1");
        actionSlotButton2 = root.Q<TemplateContainer>("ActionSlotButton2");
        actionSlotButton3 = root.Q<TemplateContainer>("ActionSlotButton3");
        actionSlotButton4 = root.Q<TemplateContainer>("ActionSlotButton4");
        passiveSlot1 = root.Q<VisualElement>("passive-slot-1");
        passiveIcon1 = root.Q<VisualElement>("passive-icon-1");
        passiveName1 = root.Q<Label>("passive-name-1");
        passiveSlot2 = root.Q<VisualElement>("passive-slot-2");
        passiveIcon2 = root.Q<VisualElement>("passive-icon-2");
        passiveName2 = root.Q<Label>("passive-name-2");
        passiveSlot3 = root.Q<VisualElement>("passive-slot-3");
        passiveIcon3 = root.Q<VisualElement>("passive-icon-3");
        passiveName3 = root.Q<Label>("passive-name-3");
        passiveSlot4 = root.Q<VisualElement>("passive-slot-4");
        passiveIcon4 = root.Q<VisualElement>("passive-icon-4");
        passiveName4 = root.Q<Label>("passive-name-4");
        closeButtonBottom = root.Q<Button>("close-button-bottom");
    }
}

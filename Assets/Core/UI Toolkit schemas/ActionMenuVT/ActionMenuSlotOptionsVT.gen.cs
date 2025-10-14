// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class ActionMenuSlotOptionsVT
{
    public const string s_actionButton = "action-button";
    public const string s_actionButtons = "action-buttons";
    public const string s_actionSlotRequiredLevel = "action-slot-required-level";
    public const string s_actionSlotRuneName = "action-slot-rune-name";
    public const string s_arrowIcon = "arrow-icon";
    public const string s_auxiliary = "auxiliary";
    public const string s_bindingItem = "binding-item";
    public const string s_carouselArrow = "carousel-arrow";
    public const string s_closeButton = "close-button";
    public const string s_closeButtonBottom = "close-button-bottom";
    public const string s_closeIcon = "close-icon";
    public const string s_content = "content";
    public const string s_defensive = "defensive";
    public const string s_descriptionSection = "description-section";
    public const string s_header = "header";
    public const string s_headerTitle = "header-title";
    public const string s_keyBinding = "key-binding";
    public const string s_mouseIcon = "mouse-icon";
    public const string s_noRune = "no-rune";
    public const string s_offensive = "offensive";
    public const string s_passiveIcon = "passive-icon";
    public const string s_passiveIconContainer = "passive-icon-container";
    public const string s_passiveName = "passive-name";
    public const string s_passiveSkillsRow = "passive-skills-row";
    public const string s_passiveSlot = "passive-slot";
    public const string s_previewBindings = "preview-bindings";
    public const string s_previewHeader = "preview-header";
    public const string s_previewIcon = "preview-icon";
    public const string s_previewInfo = "preview-info";
    public const string s_previewRune = "preview-rune";
    public const string s_previewRuneIcon = "preview-rune-icon";
    public const string s_previewRuneName = "preview-rune-name";
    public const string s_previewSkillName = "preview-skill-name";
    public const string s_primary = "primary";
    public const string s_runeIcon = "rune-icon";
    public const string s_runeItem = "rune-item";
    public const string s_runeName = "rune-name";
    public const string s_runesList = "runes-list";
    public const string s_runesSection = "runes-section";
    public const string s_runeUnactiveOverlay = "rune-unactive-overlay";
    public const string s_secondary = "secondary";
    public const string s_section = "section";
    public const string s_sectionTitle = "section-title";
    public const string s_selected = "selected";
    public const string s_skillBinding = "skill-binding";
    public const string s_skillCarousel = "skill-carousel";
    public const string s_skillCategory = "skill-category";
    public const string s_skillFrame = "skill-frame";
    public const string s_skillIcon = "skill-icon";
    public const string s_skillIconContainer = "skill-icon-container";
    public const string s_skillIconLarge = "skill-icon-large";
    public const string s_skillItem = "skill-item";
    public const string s_skillItemName = "skill-item-name";
    public const string s_skillListHorizontal = "skill-list-horizontal";
    public const string s_skillName = "skill-name";
    public const string s_skillPreview = "skill-preview";
    public const string s_skillSelectionSection = "skill-selection-section";
    public const string s_skillsGrid = "skills-grid";
    public const string s_skillSlot = "skill-slot";
    public const string s_skillsRow = "skills-row";
    public const string s_skillsWindow = "skills-window";
    public const string s_unactiveOverlay = "unactive-overlay";
    
    public Button closeButton;
    public Button swipeLeftButton;
    public VisualElement actionsContainer;
    public Button swipeRightButton;
    public VisualElement runesContainer;
    public VisualElement noRune;
    public TemplateContainer selectedAction;
    public Button acceptButton;
    public Button cancelButton;
    
    public ActionMenuSlotOptionsVT(VisualElement root)
    {
        closeButton = root.Q<Button>("close-button");
        swipeLeftButton = root.Q<Button>("SwipeLeftButton");
        actionsContainer = root.Q<VisualElement>("ActionsContainer");
        swipeRightButton = root.Q<Button>("SwipeRightButton");
        runesContainer = root.Q<VisualElement>("RunesContainer");
        noRune = root.Q<VisualElement>("NoRune");
        selectedAction = root.Q<TemplateContainer>("SelectedAction");
        acceptButton = root.Q<Button>("accept-button");
        cancelButton = root.Q<Button>("cancel-button");
    }
}

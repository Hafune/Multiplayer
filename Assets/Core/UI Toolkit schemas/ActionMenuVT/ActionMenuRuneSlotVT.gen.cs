// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class ActionMenuRuneSlotVT
{
    public const string s_actionButton = "action-button";
    public const string s_actionButtons = "action-buttons";
    public const string s_actionSlotRequiredLevel = "action-slot-required-level";
    public const string s_arrowIcon = "arrow-icon";
    public const string s_bindingItem = "binding-item";
    public const string s_carouselArrow = "carousel-arrow";
    public const string s_descriptionSection = "description-section";
    public const string s_noRune = "no-rune";
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
    public const string s_selected = "selected";
    public const string s_skillCarousel = "skill-carousel";
    public const string s_skillIconLarge = "skill-icon-large";
    public const string s_skillItem = "skill-item";
    public const string s_skillItemName = "skill-item-name";
    public const string s_skillListHorizontal = "skill-list-horizontal";
    public const string s_skillPreview = "skill-preview";
    public const string s_skillSelectionSection = "skill-selection-section";
    public const string s_unactiveOverlay = "unactive-overlay";
    
    public VisualElement container;
    public VisualElement icon;
    public Label runeName;
    public VisualElement unactiveOverlay;
    public Label requiredLevel;
    public TemplateContainer labelNewVT;
    
    public ActionMenuRuneSlotVT(VisualElement root)
    {
        container = root.Q<VisualElement>("Container");
        icon = root.Q<VisualElement>("Icon");
        runeName = root.Q<Label>("RuneName");
        unactiveOverlay = root.Q<VisualElement>("UnactiveOverlay");
        requiredLevel = root.Q<Label>("RequiredLevel");
        labelNewVT = root.Q<TemplateContainer>("LabelNewVT");
    }
}

// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class ActionBarVT
{
    public const string s_bar = "bar";
    public const string s_container = "container";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_resource_bar = "resource_bar";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_unityLabel = "unity-label";
    public const string s_unityProgressBar__background = "unity-progress-bar__background";
    public const string s_unityProgressBar__container = "unity-progress-bar__container";
    public const string s_unityProgressBar__progress = "unity-progress-bar__progress";
    public const string s_unityProgressBar__titleContainer = "unity-progress-bar__title-container";
    
    public VisualElement barArea;
    public ProgressBar hpBar;
    public ProgressBar resourceBar;
    public Label level;
    public VisualElement experienceBarContainer;
    public VisualElement experienceBar;
    public TemplateContainer spell1;
    public TemplateContainer spell2;
    public TemplateContainer spell3;
    public TemplateContainer spell4;
    public TemplateContainer spellLcm;
    public TemplateContainer spellRcm;
    public TemplateContainer spellHealingPotion;
    public VisualElement inventoryButton;
    public VisualElement teleportButtonContainer;
    public VisualElement teleportButton;
    public VisualElement actionMenuButton;
    public TemplateContainer labelNewVT;
    public VisualElement mapButton;
    public VisualElement settingsButton;
    public VisualElement removeButton;
    
    public ActionBarVT(VisualElement root)
    {
        barArea = root.Q<VisualElement>("BarArea");
        hpBar = root.Q<ProgressBar>("HpBar");
        resourceBar = root.Q<ProgressBar>("ResourceBar");
        level = root.Q<Label>("Level");
        experienceBarContainer = root.Q<VisualElement>("ExperienceBarContainer");
        experienceBar = root.Q<VisualElement>("ExperienceBar");
        spell1 = root.Q<TemplateContainer>("Spell1");
        spell2 = root.Q<TemplateContainer>("Spell2");
        spell3 = root.Q<TemplateContainer>("Spell3");
        spell4 = root.Q<TemplateContainer>("Spell4");
        spellLcm = root.Q<TemplateContainer>("SpellLcm");
        spellRcm = root.Q<TemplateContainer>("SpellRcm");
        spellHealingPotion = root.Q<TemplateContainer>("SpellHealingPotion");
        inventoryButton = root.Q<VisualElement>("InventoryButton");
        teleportButtonContainer = root.Q<VisualElement>("TeleportButtonContainer");
        teleportButton = root.Q<VisualElement>("TeleportButton");
        actionMenuButton = root.Q<VisualElement>("ActionMenuButton");
        labelNewVT = root.Q<TemplateContainer>("LabelNewVT");
        mapButton = root.Q<VisualElement>("MapButton");
        settingsButton = root.Q<VisualElement>("SettingsButton");
        removeButton = root.Q<VisualElement>("RemoveButton");
    }
}

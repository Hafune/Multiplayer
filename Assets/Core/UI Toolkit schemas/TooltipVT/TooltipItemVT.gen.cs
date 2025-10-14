// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class TooltipItemVT
{
    public const string s_blue_rarity_title_color = "blue_rarity_title_color";
    public const string s_compare_row = "compare_row";
    public const string s_compare_values_green_color = "compare_values_green_color";
    public const string s_default_text_size = "default_text_size";
    public const string s_double_dots = "double_dots";
    public const string s_gray_rarity_title_color = "gray_rarity_title_color";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_required_level_red = "required_level_red";
    public const string s_secondary_labels_color = "secondary_labels_color";
    public const string s_size_1x1 = "size_1x1";
    public const string s_size_1x2 = "size_1x2";
    public const string s_system_labels = "system_labels";
    public const string s_system_labels_color = "system_labels_color";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_value_labels_color = "value_labels_color";
    public const string s_yellow_rarity_title_color = "yellow_rarity_title_color";
    
    public VisualElement rootContainer;
    public Label onPlayerMark;
    public Label title;
    public VisualElement iconContainer;
    public VisualElement iconBackground;
    public VisualElement icon;
    public Label itemType;
    public Label itemSlot;
    public Label mainLabel;
    public Label mainLabelDamageDescription;
    public Label mainLabelBlockDescription;
    public VisualElement extraContainerDamage;
    public Label damageValue;
    public Label damageValueDescription;
    public VisualElement attackSpeedContainer;
    public Label attackSpeedValue;
    public Label attackSpeedValueDescription;
    public VisualElement extraContainerBlock;
    public Label blockChanceValue;
    public Label blockChanceDescription;
    public Label blockValue;
    public Label blockValueDescription;
    public VisualElement primaryContainer;
    public Label primaryValues;
    public VisualElement secondaryContainer;
    public Label secondaryValues;
    public VisualElement compareContainer;
    public Label label;
    public TemplateContainer compareDamageContainer;
    public TemplateContainer compareToughnessContainer;
    public TemplateContainer compareRecoveryContainer;
    public Label cost;
    public Label costValue;
    public VisualElement requiredLevelContainer;
    public Label requiredLevel;
    public Label requiredLevelValue;
    
    public TooltipItemVT(VisualElement root)
    {
        rootContainer = root.Q<VisualElement>("RootContainer");
        onPlayerMark = root.Q<Label>("OnPlayerMark");
        title = root.Q<Label>("Title");
        iconContainer = root.Q<VisualElement>("IconContainer");
        iconBackground = root.Q<VisualElement>("IconBackground");
        icon = root.Q<VisualElement>("Icon");
        itemType = root.Q<Label>("ItemType");
        itemSlot = root.Q<Label>("ItemSlot");
        mainLabel = root.Q<Label>("MainLabel");
        mainLabelDamageDescription = root.Q<Label>("MainLabelDamageDescription");
        mainLabelBlockDescription = root.Q<Label>("MainLabelBlockDescription");
        extraContainerDamage = root.Q<VisualElement>("ExtraContainerDamage");
        damageValue = root.Q<Label>("DamageValue");
        damageValueDescription = root.Q<Label>("DamageValueDescription");
        attackSpeedContainer = root.Q<VisualElement>("AttackSpeedContainer");
        attackSpeedValue = root.Q<Label>("AttackSpeedValue");
        attackSpeedValueDescription = root.Q<Label>("AttackSpeedValueDescription");
        extraContainerBlock = root.Q<VisualElement>("ExtraContainerBlock");
        blockChanceValue = root.Q<Label>("BlockChanceValue");
        blockChanceDescription = root.Q<Label>("BlockChanceDescription");
        blockValue = root.Q<Label>("BlockValue");
        blockValueDescription = root.Q<Label>("BlockValueDescription");
        primaryContainer = root.Q<VisualElement>("PrimaryContainer");
        primaryValues = root.Q<Label>("PrimaryValues");
        secondaryContainer = root.Q<VisualElement>("SecondaryContainer");
        secondaryValues = root.Q<Label>("SecondaryValues");
        compareContainer = root.Q<VisualElement>("CompareContainer");
        label = root.Q<Label>("Label");
        compareDamageContainer = root.Q<TemplateContainer>("CompareDamageContainer");
        compareToughnessContainer = root.Q<TemplateContainer>("CompareToughnessContainer");
        compareRecoveryContainer = root.Q<TemplateContainer>("CompareRecoveryContainer");
        cost = root.Q<Label>("Cost");
        costValue = root.Q<Label>("CostValue");
        requiredLevelContainer = root.Q<VisualElement>("RequiredLevelContainer");
        requiredLevel = root.Q<Label>("RequiredLevel");
        requiredLevelValue = root.Q<Label>("RequiredLevelValue");
    }
}

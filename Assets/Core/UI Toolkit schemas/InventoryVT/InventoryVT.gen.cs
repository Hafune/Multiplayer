// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class InventoryVT
{
    public const string s_accessories_size = "accessories_size";
    public const string s_cell = "cell";
    public const string s_cell_size_1x2 = "cell_size_1x2";
    public const string s_equipment_slots = "equipment_slots";
    public const string s_item_background_cell = "item_background_cell";
    public const string s_item_cell = "item_cell";
    public const string s_item_cell_Image = "item_cell_Image";
    public const string s_main_property_size = "main_property_size";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_property_container = "property_container";
    public const string s_property_name = "property_name";
    public const string s_property_value = "property_value";
    public const string s_row = "row";
    public const string s_second_property_container = "second_property_container";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_waist_size = "waist_size";
    
    public Core.FocusableVisualElement inventoryArea;
    public Label attackSpeedValue;
    public Label criticalChanceValue;
    public Label criticalDamageValue;
    public Label recoveryTimeReductionValue;
    public Label resourceCostsReductionValue;
    public Label resistanceAllValue;
    public Label hitPointMaxValue;
    public Label vitalityPercentValue;
    public Label healingPerHitValue;
    public Label healingPerSecondValue;
    public Label levelValue;
    public Label strengthValue;
    public Label vitalityValue;
    public Label armorValue;
    public VisualElement damageContainer;
    public Label damage;
    public VisualElement toughnessContainer;
    public Label toughness;
    public VisualElement recoveryContainer;
    public Label recovery;
    public VisualElement inventoryContainer;
    public VisualElement itemsBackgroundsContainer;
    public VisualElement row;
    public VisualElement itemBackgroundCellContainer;
    public VisualElement itemBackground;
    public VisualElement itemsContainer;
    public VisualElement itemCellContainer;
    public VisualElement itemCell;
    public VisualElement overlayContainer;
    public Core.FocusableVisualElement overlayCell;
    public TemplateContainer head;
    public TemplateContainer shoulders;
    public TemplateContainer neck;
    public TemplateContainer mainHand;
    public TemplateContainer chest;
    public TemplateContainer offHand;
    public TemplateContainer hands;
    public TemplateContainer fingerLeft;
    public TemplateContainer fingerRight;
    public TemplateContainer legs;
    public TemplateContainer feet;
    public TemplateContainer waist;
    public TemplateContainer wrists;
    public VisualElement currencyContainer;
    public Label currency;
    public VisualElement closeButton;
    
    public InventoryVT(VisualElement root)
    {
        inventoryArea = root.Q<Core.FocusableVisualElement>("InventoryArea");
        attackSpeedValue = root.Q<Label>("AttackSpeedValue");
        criticalChanceValue = root.Q<Label>("CriticalChanceValue");
        criticalDamageValue = root.Q<Label>("CriticalDamageValue");
        recoveryTimeReductionValue = root.Q<Label>("RecoveryTimeReductionValue");
        resourceCostsReductionValue = root.Q<Label>("ResourceCostsReductionValue");
        resistanceAllValue = root.Q<Label>("ResistanceAllValue");
        hitPointMaxValue = root.Q<Label>("HitPointMaxValue");
        vitalityPercentValue = root.Q<Label>("VitalityPercentValue");
        healingPerHitValue = root.Q<Label>("HealingPerHitValue");
        healingPerSecondValue = root.Q<Label>("HealingPerSecondValue");
        levelValue = root.Q<Label>("LevelValue");
        strengthValue = root.Q<Label>("StrengthValue");
        vitalityValue = root.Q<Label>("VitalityValue");
        armorValue = root.Q<Label>("ArmorValue");
        damageContainer = root.Q<VisualElement>("DamageContainer");
        damage = root.Q<Label>("Damage");
        toughnessContainer = root.Q<VisualElement>("ToughnessContainer");
        toughness = root.Q<Label>("Toughness");
        recoveryContainer = root.Q<VisualElement>("RecoveryContainer");
        recovery = root.Q<Label>("Recovery");
        inventoryContainer = root.Q<VisualElement>("InventoryContainer");
        itemsBackgroundsContainer = root.Q<VisualElement>("ItemsBackgroundsContainer");
        row = root.Q<VisualElement>("Row");
        itemBackgroundCellContainer = root.Q<VisualElement>("ItemBackgroundCellContainer");
        itemBackground = root.Q<VisualElement>("ItemBackground");
        itemsContainer = root.Q<VisualElement>("ItemsContainer");
        itemCellContainer = root.Q<VisualElement>("ItemCellContainer");
        itemCell = root.Q<VisualElement>("ItemCell");
        overlayContainer = root.Q<VisualElement>("OverlayContainer");
        overlayCell = root.Q<Core.FocusableVisualElement>("OverlayCell");
        head = root.Q<TemplateContainer>("Head");
        shoulders = root.Q<TemplateContainer>("Shoulders");
        neck = root.Q<TemplateContainer>("Neck");
        mainHand = root.Q<TemplateContainer>("MainHand");
        chest = root.Q<TemplateContainer>("Chest");
        offHand = root.Q<TemplateContainer>("OffHand");
        hands = root.Q<TemplateContainer>("Hands");
        fingerLeft = root.Q<TemplateContainer>("FingerLeft");
        fingerRight = root.Q<TemplateContainer>("FingerRight");
        legs = root.Q<TemplateContainer>("Legs");
        feet = root.Q<TemplateContainer>("Feet");
        waist = root.Q<TemplateContainer>("Waist");
        wrists = root.Q<TemplateContainer>("Wrists");
        currencyContainer = root.Q<VisualElement>("CurrencyContainer");
        currency = root.Q<Label>("Currency");
        closeButton = root.Q<VisualElement>("CloseButton");
    }
}

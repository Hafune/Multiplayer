// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class ItemEnhancementVT
{
    public const string s_blue_rarity_title_color = "blue_rarity_title_color";
    public const string s_closeButton = "close-button";
    public const string s_contentArea = "content-area";
    public const string s_default_text_size = "default_text_size";
    public const string s_double_dots = "double_dots";
    public const string s_enhanceButton = "enhance-button";
    public const string s_enhanceDecorButton = "enhance-decor-button";
    public const string s_goldAmount = "gold-amount";
    public const string s_goldArea = "gold-area";
    public const string s_goldContainer = "gold-container";
    public const string s_goldIcon = "gold-icon";
    public const string s_goldLabel = "gold-label";
    public const string s_gray_rarity_title_color = "gray_rarity_title_color";
    public const string s_header = "header";
    public const string s_headerTitle = "header-title";
    public const string s_itemSlot = "item-slot";
    public const string s_itemSlotArea = "item-slot-area";
    public const string s_leftPanel = "left-panel";
    public const string s_main_value_size = "main_value_size";
    public const string s_mainContainer = "main-container";
    public const string s_my_button = "my_button";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_panelsContainer = "panels-container";
    public const string s_possiblePropertyItem = "possible-property-item";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_propertiesArea = "properties-area";
    public const string s_propertiesContent = "properties-content";
    public const string s_propertiesHeader = "properties-header";
    public const string s_propertiesHeaderTitle = "properties-header-title";
    public const string s_propertiesTitle = "properties-title";
    public const string s_propertyItem = "property-item";
    public const string s_required_level_red = "required_level_red";
    public const string s_resourceAmount = "resource-amount";
    public const string s_resourceIcon = "resource-icon";
    public const string s_resourceItem = "resource-item";
    public const string s_resourcesArea = "resources-area";
    public const string s_resourcesList = "resources-list";
    public const string s_resourcesTitle = "resources-title";
    public const string s_rightPanel = "right-panel";
    public const string s_secondary_labels_color = "secondary_labels_color";
    public const string s_selectedProperty = "selected-property";
    public const string s_size_1x1 = "size_1x1";
    public const string s_size_1x2 = "size_1x2";
    public const string s_system_labels = "system_labels";
    public const string s_system_labels_color = "system_labels_color";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_value_labels_color = "value_labels_color";
    public const string s_yellow_rarity_title_color = "yellow_rarity_title_color";
    
    public VisualElement mainContainer;
    public VisualElement panelsContainer;
    public VisualElement leftPanel;
    public Button closeButton;
    public VisualElement itemSlotContainer;
    public VisualElement itemSlot;
    public Label selectItem;
    public Label selectProperty;
    public Label changeProperty;
    public Label selectNewProperty;
    public VisualElement propertiesList;
    public Label descriptionImptyItem;
    public Label descriptionChangedItem;
    public Label resourcesTitle;
    public VisualElement resourcesList;
    public Button selectItemDecorButton;
    public Button selectPropertyDecorButton;
    public VisualElement enhanceButton;
    public Label requiredGold;
    public Button selectNewPropertyButton;
    public Label goldAmount;
    public VisualElement rightPanel;
    public Button propertiesCloseButton;
    public VisualElement possiblePropertiesList;
    
    public ItemEnhancementVT(VisualElement root)
    {
        mainContainer = root.Q<VisualElement>("MainContainer");
        panelsContainer = root.Q<VisualElement>("PanelsContainer");
        leftPanel = root.Q<VisualElement>("LeftPanel");
        closeButton = root.Q<Button>("CloseButton");
        itemSlotContainer = root.Q<VisualElement>("ItemSlotContainer");
        itemSlot = root.Q<VisualElement>("ItemSlot");
        selectItem = root.Q<Label>("SelectItem");
        selectProperty = root.Q<Label>("SelectProperty");
        changeProperty = root.Q<Label>("ChangeProperty");
        selectNewProperty = root.Q<Label>("SelectNewProperty");
        propertiesList = root.Q<VisualElement>("PropertiesList");
        descriptionImptyItem = root.Q<Label>("DescriptionImptyItem");
        descriptionChangedItem = root.Q<Label>("DescriptionChangedItem");
        resourcesTitle = root.Q<Label>("ResourcesTitle");
        resourcesList = root.Q<VisualElement>("ResourcesList");
        selectItemDecorButton = root.Q<Button>("SelectItemDecorButton");
        selectPropertyDecorButton = root.Q<Button>("SelectPropertyDecorButton");
        enhanceButton = root.Q<VisualElement>("EnhanceButton");
        requiredGold = root.Q<Label>("RequiredGold");
        selectNewPropertyButton = root.Q<Button>("SelectNewPropertyButton");
        goldAmount = root.Q<Label>("GoldAmount");
        rightPanel = root.Q<VisualElement>("RightPanel");
        propertiesCloseButton = root.Q<Button>("PropertiesCloseButton");
        possiblePropertiesList = root.Q<VisualElement>("PossiblePropertiesList");
    }
}

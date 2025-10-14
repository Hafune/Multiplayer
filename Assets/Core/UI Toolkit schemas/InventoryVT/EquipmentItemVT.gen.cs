// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class EquipmentItemVT
{
    public const string s_accessories_size = "accessories_size";
    public const string s_cell = "cell";
    public const string s_cell_size_1x2 = "cell_size_1x2";
    public const string s_equipment_slots = "equipment_slots";
    public const string s_item_background_cell = "item_background_cell";
    public const string s_item_cell = "item_cell";
    public const string s_item_cell_Image = "item_cell_Image";
    public const string s_main_property_size = "main_property_size";
    public const string s_property_container = "property_container";
    public const string s_property_name = "property_name";
    public const string s_property_value = "property_value";
    public const string s_row = "row";
    public const string s_second_property_container = "second_property_container";
    public const string s_waist_size = "waist_size";
    
    public Core.FocusableVisualElement focusable;
    public VisualElement background;
    public VisualElement icon;
    
    public EquipmentItemVT(VisualElement root)
    {
        focusable = root.Q<Core.FocusableVisualElement>("Focusable");
        background = root.Q<VisualElement>("Background");
        icon = root.Q<VisualElement>("Icon");
    }
}

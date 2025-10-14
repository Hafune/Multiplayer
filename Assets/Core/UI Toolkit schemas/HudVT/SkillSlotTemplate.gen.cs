// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class SkillSlotTemplate
{
    public const string s_cooldown_radial_transform = "cooldown_radial_transform";
    public const string s_disable_color = "disable_color";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    
    public VisualElement spellIcon;
    public VisualElement costOverlay;
    public VisualElement cooldownOverlay;
    public VisualElement cooldownLeft;
    public VisualElement cooldownRight;
    public Label charge;
    public VisualElement hotKeyIcon;
    public Label hotKeyText;
    
    public SkillSlotTemplate(VisualElement root)
    {
        spellIcon = root.Q<VisualElement>("SpellIcon");
        costOverlay = root.Q<VisualElement>("CostOverlay");
        cooldownOverlay = root.Q<VisualElement>("CooldownOverlay");
        cooldownLeft = root.Q<VisualElement>("CooldownLeft");
        cooldownRight = root.Q<VisualElement>("CooldownRight");
        charge = root.Q<Label>("Charge");
        hotKeyIcon = root.Q<VisualElement>("HotKeyIcon");
        hotKeyText = root.Q<Label>("HotKeyText");
    }
}

// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class PlayerInfoRow
{
    public const string s_extradata_sprite = "extradata_sprite";
    public const string s_extradata_value = "extradata_value";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_player_frame = "player_frame";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_row = "row";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    
    public Label rank;
    public Label name;
    public Label score;
    public VisualElement extraDataContainer;
    public Label level;
    public Label damage;
    public Label toughness;
    public Label recovery;
    
    public PlayerInfoRow(VisualElement root)
    {
        rank = root.Q<Label>("Rank");
        name = root.Q<Label>("Name");
        score = root.Q<Label>("Score");
        extraDataContainer = root.Q<VisualElement>("ExtraDataContainer");
        level = root.Q<Label>("level");
        damage = root.Q<Label>("damage");
        toughness = root.Q<Label>("toughness");
        recovery = root.Q<Label>("recovery");
    }
}

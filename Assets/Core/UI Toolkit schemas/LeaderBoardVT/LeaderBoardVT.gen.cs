// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class LeaderBoardVT
{
    public const string s_leaderboard_container = "leaderboard_container";
    public const string s_leaderboard_container_hidden = "leaderboard_container_hidden";
    public const string s_list = "list";
    public const string s_listContainer = "listContainer";
    public const string s_main_value_size = "main_value_size";
    public const string s_other_labels_color = "other_labels_color";
    public const string s_primary_labels_color = "primary_labels_color";
    public const string s_season_container = "season_container";
    public const string s_season_container_hidden = "season_container_hidden";
    public const string s_season_toggle = "season_toggle";
    public const string s_seasonTitle = "season-title";
    public const string s_seasonTitleText = "season-title-text";
    public const string s_textColorBase = "text-color-base";
    public const string s_textColorBase_Label = "text-color-base Label";
    public const string s_textFontBase = "text-font-base";
    public const string s_title_size = "title_size";
    public const string s_toggle_arrow_left = "toggle_arrow_left";
    public const string s_toggle_arrow_right = "toggle_arrow_right";
    
    public VisualElement seasonRewardButton;
    public Label rewardValue;
    public VisualElement showHideToggle;
    public VisualElement showHideArrow;
    public VisualElement leaderboard;
    public VisualElement previousSeasonContainer;
    public ListView previousTopList;
    public ListView previousOtherList;
    public VisualElement previousSeasonToggle;
    public VisualElement previousSeasonArrow;
    public VisualElement currentSeasonContainer;
    public ListView topList;
    public ListView otherList;
    public VisualElement loginButton;
    
    public LeaderBoardVT(VisualElement root)
    {
        seasonRewardButton = root.Q<VisualElement>("SeasonRewardButton");
        rewardValue = root.Q<Label>("RewardValue");
        showHideToggle = root.Q<VisualElement>("ShowHideToggle");
        showHideArrow = root.Q<VisualElement>("ShowHideArrow");
        leaderboard = root.Q<VisualElement>("Leaderboard");
        previousSeasonContainer = root.Q<VisualElement>("PreviousSeasonContainer");
        previousTopList = root.Q<ListView>("PreviousTopList");
        previousOtherList = root.Q<ListView>("PreviousOtherList");
        previousSeasonToggle = root.Q<VisualElement>("PreviousSeasonToggle");
        previousSeasonArrow = root.Q<VisualElement>("PreviousSeasonArrow");
        currentSeasonContainer = root.Q<VisualElement>("CurrentSeasonContainer");
        topList = root.Q<ListView>("TopList");
        otherList = root.Q<ListView>("OtherList");
        loginButton = root.Q<VisualElement>("LoginButton");
    }
}

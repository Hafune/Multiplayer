// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class SidebarVT
{
    
    
    public VisualElement sidebarLeft;
    public VisualElement sidebarRight;
    public VisualElement questsContainer;
    public VisualElement progressBarContainer;
    
    public SidebarVT(VisualElement root)
    {
        sidebarLeft = root.Q<VisualElement>("SidebarLeft");
        sidebarRight = root.Q<VisualElement>("SidebarRight");
        questsContainer = root.Q<VisualElement>("QuestsContainer");
        progressBarContainer = root.Q<VisualElement>("ProgressBarContainer");
    }
}

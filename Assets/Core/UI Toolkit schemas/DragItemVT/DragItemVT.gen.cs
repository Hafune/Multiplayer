// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class DragItemVT
{
    public const string s_default_size = "default_size";
    public const string s_size_1x2 = "size_1x2";
    
    public VisualElement pivot;
    public VisualElement icon;
    
    public DragItemVT(VisualElement root)
    {
        pivot = root.Q<VisualElement>("Pivot");
        icon = root.Q<VisualElement>("Icon");
    }
}

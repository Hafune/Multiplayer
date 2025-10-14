// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class PopConfirmVT
{
    public const string s_button = "button";
    public const string s_buttonIcon = "button-icon";
    public const string s_buttonText = "button-text";
    
    public VisualElement popConfirmWrapper;
    public Label message;
    public VisualElement submitButton;
    public VisualElement cancelButton;
    
    public PopConfirmVT(VisualElement root)
    {
        popConfirmWrapper = root.Q<VisualElement>("PopConfirmWrapper");
        message = root.Q<Label>("Message");
        submitButton = root.Q<VisualElement>("SubmitButton");
        cancelButton = root.Q<VisualElement>("CancelButton");
    }
}

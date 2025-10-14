// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

public class LoadingErrorVT
{
    
    
    public Label errorTitle;
    public Label errorMessage;
    public Label warning;
    public Button buttonRefetch;
    public Button buttonIgnore;
    
    public LoadingErrorVT(VisualElement root)
    {
        errorTitle = root.Q<Label>("ErrorTitle");
        errorMessage = root.Q<Label>("ErrorMessage");
        warning = root.Q<Label>("Warning");
        buttonRefetch = root.Q<Button>("ButtonRefetch");
        buttonIgnore = root.Q<Button>("ButtonIgnore");
    }
}

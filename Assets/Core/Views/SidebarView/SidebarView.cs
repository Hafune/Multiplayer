using UnityEngine.UIElements;

namespace Core.Views
{
    public class SidebarView : AbstractUIDocumentView
    {
        public VisualElement QuestsContainer { get; private set; }

        protected override void OnAwake()
        {
            var sidebar = new SidebarVT(RootVisualElement);
            QuestsContainer = sidebar.questsContainer;
            DisplayFlex();
        }
    }
}
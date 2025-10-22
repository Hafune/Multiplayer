using System.Collections.Generic;

namespace Core.Services
{
    public partial class ActionBarService
    {
        private class ServiceData
        {
            public string[] actionKeys = new[]
            {
                string.Empty, // ActionLinkButton1Component
                string.Empty, // ActionLinkButton2Component
                string.Empty, // ActionLinkButton3Component
                string.Empty, // ActionLinkButton4Component
                "barb_bash", // ActionLinkMouseLeftComponent
                string.Empty, // ActionLinkMouseRightComponent
            };

            public Dictionary<string, string> actionRuneKeys = new();
        }
    }
}
using Core.Services;
using Core.Views;

namespace Core
{
    public class AimView : AbstractUIDocumentView
    {
        protected override void OnAwake()
        {
            var root = new AimVT(RootVisualElement);
            context.Resolve<AimService>().SetAim(root.point);
            DisplayFlex();
        }
    }
}
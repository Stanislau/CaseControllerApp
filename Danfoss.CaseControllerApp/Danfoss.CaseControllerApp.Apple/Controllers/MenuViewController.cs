using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    [MvxPanelPresentation(MvxPanelEnum.Right, MvxPanelHintType.ActivePanel, false)]
    public class MenuViewController : BaseViewController<RootViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.FromRGB(0xE1, 0x00, 0x0E);

            var child = new UIView();
            child.BackgroundColor = UIColor.Blue;
            View.Add(child);

            AddFluentConstraints(
                child.AtTopOfParent(20),
                child.AtLeftOfParent(),
                child.AtRightOfParent(),
                child.AtBottomOfParent()
                );

            ViewModel.NavigateTo(0);

        }
    }
}
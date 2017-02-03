using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    [MvxPanelPresentation(MvxPanelEnum.Right, MvxPanelHintType.ActivePanel, false)]
    public class MenuViewController : MvxViewController<RootViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Red;

            ViewModel.NavigateTo(0);
        }
    }
}
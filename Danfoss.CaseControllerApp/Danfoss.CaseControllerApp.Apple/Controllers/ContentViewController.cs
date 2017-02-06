using Danfoss.CaseControllerApp.Core.Services.Helpers;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.Platform;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class ContentViewController<T> : BaseViewController<T> where T : ChildViewModel
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            NavigationController.NavigationBarHidden = true;

            Mvx.Resolve<IRootViewModelNotifier>().ViewModelChanged(ViewModel);
        }
    }
}
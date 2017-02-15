using Danfoss.CaseControllerApp.Core.Services.Helpers;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using MvvmCross.Platform;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class WizardViewController : MvxPageViewController<WizardViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Mvx.Resolve<IRootViewModelNotifier>().ViewModelChanged(ViewModel);
        }
    }
}
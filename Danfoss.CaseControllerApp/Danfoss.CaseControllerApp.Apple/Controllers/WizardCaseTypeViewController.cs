using System.Collections.Generic;
using Danfoss.CaseControllerApp.Apple.InterfaceBuilder;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class WizardCaseTypeViewController : WizardStepViewController<WizardCaseTypeViewController, WizardCaseTypeViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Black;
        }

        protected override IEnumerable<Element> CreateUI(UserInterface<WizardCaseTypeViewController, WizardCaseTypeViewModel> ui)
        {
            yield break;
        }
    }
}
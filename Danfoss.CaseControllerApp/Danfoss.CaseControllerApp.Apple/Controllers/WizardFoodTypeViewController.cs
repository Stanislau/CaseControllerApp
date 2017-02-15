using System.Collections.Generic;
using Danfoss.CaseControllerApp.Apple.InterfaceBuilder;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class WizardFoodTypeViewController : WizardStepViewController<WizardFoodTypeViewController, WizardFoodTypeViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Gray;
        }

        protected override IEnumerable<Element> CreateUI(UserInterface<WizardFoodTypeViewController, WizardFoodTypeViewModel> ui)
        {
            yield break;
        }
    }
}
using System.Collections.Generic;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Apple.InterfaceBuilder;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class WizardApplicationTypeViewController : WizardStepViewController<WizardApplicationTypeViewController, WizardApplicationTypeViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Brown;
        }

        protected override IEnumerable<Element> CreateUI(UserInterface<WizardApplicationTypeViewController, WizardApplicationTypeViewModel> ui)
        {
            yield return ui.Element(new UILabel())
                .Set(h => h.TextColor = UIColor.Yellow)
                .Constraints((h, title) => new[]
                {
                    h.Top().EqualTo(0).BottomOf(title),
                    h.CenterXOfParent(),
                    h.WidthOfSelf(),
                    h.HeightOfSelf()
                })
                .Bindings((h, set) => set.Bind(h).To(vm => vm.Help));
        }
    }
}
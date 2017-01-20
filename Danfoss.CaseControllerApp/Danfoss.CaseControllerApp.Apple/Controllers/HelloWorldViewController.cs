using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Core.ViewModels;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class HelloWorldViewController : BaseViewController<HelloWorldViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            var helloWorldLabel = new UILabel();
            helloWorldLabel.TextColor = UIColor.Black;
            Add(helloWorldLabel);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            AddFluentConstraints(
                helloWorldLabel.CenterXOfParent(), 
                helloWorldLabel.CenterYOfParent());

            AddFluentBindings(set =>
            {
                set.Bind(helloWorldLabel).To(vm => vm.Title);
            });
        }
    }
}
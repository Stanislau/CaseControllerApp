using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class SecondViewController : ContentViewController<SecondViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Cyan;
        }
    }
}
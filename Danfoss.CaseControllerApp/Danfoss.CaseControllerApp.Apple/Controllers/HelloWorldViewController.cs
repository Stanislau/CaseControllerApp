using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Core.ViewModels;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class HelloWorldViewController : BaseViewController<DeviceListViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            var helloWorldLabel = new UILabel();
            helloWorldLabel.TextColor = UIColor.Black;
            Add(helloWorldLabel);

            var cancel = new UIButton();
            cancel.SetTitle("Cancel", UIControlState.Normal);
            cancel.SetTitleColor(UIColor.Red, UIControlState.Normal);
            Add(cancel);

            var resume = new UIButton();
            resume.SetTitle("Start", UIControlState.Normal);
            resume.SetTitleColor(UIColor.Green, UIControlState.Normal);
            Add(resume);

            AddFluentConstraints(
                helloWorldLabel.CenterXOfParent(), 
                helloWorldLabel.CenterYOfParent(),
                helloWorldLabel.WidthOfSelf(),
                helloWorldLabel.HeightOfSelf(),
                
                cancel.Top().EqualTo().BottomOf(helloWorldLabel),
                cancel.CenterXOfParent(),
                cancel.WidthOfSelf(),
                cancel.HeightOfSelf(),

                resume.Top().EqualTo().BottomOf(cancel),
                resume.CenterXOfParent(),
                resume.WidthOfSelf(),
                resume.HeightOfSelf()

                );

            //AddFluentBindings(set =>
            //{
            //    set.Bind(helloWorldLabel).To(vm => vm.Title);

            //    set.Bind(cancel).To(x => x.Cancel);

            //    set.Bind(resume).To(x => x.Scan);
            //});
        }
    }
}
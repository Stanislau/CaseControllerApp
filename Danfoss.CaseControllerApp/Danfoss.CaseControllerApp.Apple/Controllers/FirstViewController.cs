using System;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using Daven.SyntaxExtensions;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.Platform;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class FirstViewController : ContentViewController<FirstViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Green;

            var b = new UIButton(UIButtonType.System);
            b.SetTitleColor(UIColor.Black, UIControlState.Normal);
            b.SetTitle("Toggle left", UIControlState.Normal);
            b.TouchUpInside += BOnTouchUpInside;
            Add(b);

            var b2 = new UIButton(UIButtonType.System);
            b2.SetTitleColor(UIColor.Black, UIControlState.Normal);
            b2.SetTitle("Toggle right", UIControlState.Normal);
            b2.TouchUpInside += BOnTouchUpInside2;
            Add(b2);

            var next = new UIButton(UIButtonType.System);
            next.SetTitleColor(UIColor.Black, UIControlState.Normal);
            next.SetTitle("next", UIControlState.Normal);
            next.TouchUpInside += Next;
            Add(next);

            AddFluentConstraints(
                b.AtTopOfParent(),
                b.CenterXOfParent(),
                b.HeightOfSelf(),
                b.WidthOfSelf(),

                b2.Top().EqualTo().BottomOf(b),
                b2.CenterXOfParent(),
                b2.HeightOfSelf(),
                b2.WidthOfSelf(),

                next.Top().EqualTo().BottomOf(b2),
                next.CenterXOfParent(),
                next.HeightOfSelf(),
                next.WidthOfSelf()
                );
        }

        private void Next(object sender, EventArgs e)
        {
            ViewModel.Next();
        }

        private void BOnTouchUpInside2(object sender, EventArgs e)
        {
            var panel = Mvx.Resolve<IMvxSideMenu>().CastInstanceTo<MvxSidebarPanelController>();
            panel.RightSidebarController.Disabled = !panel.RightSidebarController.Disabled;
            sender.CastInstanceTo<UIButton>().SetTitle("Toggle right " + (panel.RightSidebarController.Disabled.ToString()), UIControlState.Normal);
        }

        private void BOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            var panel = Mvx.Resolve<IMvxSideMenu>().CastInstanceTo<MvxSidebarPanelController>();
            panel.LeftSidebarController.Disabled = !panel.LeftSidebarController.Disabled;
            sender.CastInstanceTo<UIButton>().SetTitle("Toggle left " + (panel.LeftSidebarController.Disabled.ToString()), UIControlState.Normal);
        }
    }
}
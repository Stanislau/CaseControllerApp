using System;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Apple.Infrastructure;
using Danfoss.CaseControllerApp.Apple.InterfaceBuilder;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using Daven.SyntaxExtensions;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.iOS.Views;
using MvvmCross.Platform;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    [IgnoreView]
    public class HeaderViewController : MvxViewController<RootViewModel>
    {
        private MvxSidebarPanelController _sideMenu;
        public UINavigationController Content { get; set; }

        public HeaderViewController()
        {
            Content = new CachedNavigationController(new UIViewController()) {NavigationBarHidden = true};
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _sideMenu = Mvx.Resolve<IMvxSideMenu>().CastInstanceTo<MvxSidebarPanelController>();

            View.BackgroundColor = UIColor.FromRGB(0xE1, 0x00, 0x0E);

            NavigationController.NavigationBarHidden = true;

            AddChildViewController(Content);

            var ui = new UserInterface<HeaderViewController, RootViewModel>(this, View);

            ui.Build( 
                ui.Element<UIView>("deviceStatus")
                    .Set(deviceStatus => deviceStatus.BackgroundColor = UIColor.FromRGB(0xE1, 0x00, 0x0E))
                    .Constraints(deviceStatus => new[]
                    {
                        deviceStatus.AtTopOfParent().Plus(20),
                        deviceStatus.AtLeftOfParent(),
                        deviceStatus.AtRightOfParent(),
                        deviceStatus.Height().EqualTo(20),
                    }),
                ui.Element<UIView>("navbar")
                    .Set(navbar => navbar.BackgroundColor = UIColor.White)
                    .Constraints((navbar, deviceStatus) => new[]
                    {
                        navbar.Top().EqualTo(0).BottomOf(deviceStatus),
                        navbar.AtLeftOfParent(0),
                        navbar.AtRightOfParent(0),
                        navbar.Height().EqualTo(50),
                    })
                    .Children(
                        ui.Element(new UIButton(UIButtonType.Custom))
                            .Set(back =>
                            {
                                back.SetImage(UIImage.FromBundle("icon-back"), UIControlState.Normal);
                            })
                            .Constraints(back => new []
                            {
                                back.AtLeftOfParent(),
                                back.CenterYOfParent(),
                                back.HeightOfParent(),
                                back.Width().EqualTo().HeightOf(back)
                            })
                            .Bindings((back, set) =>
                            {
                                set.Bind(back).To(vm => vm.BackCommand);
                                set.Bind(back).For("Visible").To(vm => vm.IsBackDisplayed);
                            }),

                        ui.Element<UILabel>()
                            .Set(title => title.TintColor = UIColor.Black)
                            .Constraints(title => new[]
                            {
                                title.CenterXOfParent(),
                                title.CenterYOfParent(),
                                title.WidthOfSelf(),
                                title.HeightOfSelf()
                            })
                            .Bindings((title, set) => set.Bind(title).To(x => x.Title)),

                        ui.Element(new UIButton(UIButtonType.Custom))
                            .Set(menu =>
                            {
                                menu.SetImage(UIImage.FromBundle("icon-menu"), UIControlState.Normal);
                                menu.TouchUpInside += MenuOnTouchUpInside;
                            })
                            .Constraints(menu => new[]
                            {
                                menu.AtRightOfParent(),
                                menu.CenterYOfParent(),
                                menu.HeightOfParent(),
                                menu.Width().EqualTo().HeightOf(menu)
                            })
                            .Bindings((menu, set) =>
                            {
                                set.Bind(menu).For("Visible").To(vm => vm.SideNavigationEnabled);
                                
                                set.Bind(this)
                                    .For(x => x.IsSwipeEnabled)
                                    .To(vm => vm.SideNavigationEnabled);
                            })
                    ),

                ui.Element(Content.View)
                    .Constraints((contentView, navbar) => new[]
                    {
                        contentView.Top().EqualTo(0).BottomOf(navbar),
                        contentView.AtLeftOfParent(0),
                        contentView.AtRightOfParent(0),
                        contentView.AtBottomOfParent(0)
                    })
                );
        }

        public bool IsSwipeEnabled
        {
            get { return _sideMenu.RightSidebarController.Disabled == false; }
            set
            {
                _sideMenu.RightSidebarController.Disabled = !value;
            }
        }

        private void MenuOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            Mvx.Resolve<IMvxSideMenu>().Open(MvxPanelEnum.Right);
        }
    }
}
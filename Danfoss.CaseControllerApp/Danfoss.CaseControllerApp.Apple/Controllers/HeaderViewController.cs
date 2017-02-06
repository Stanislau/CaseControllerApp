using System;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Apple.Infrastructure;
using Danfoss.CaseControllerApp.Apple.InterfaceBuilder;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    [IgnoreView]
    public class HeaderViewController : MvxViewController<RootViewModel>
    {
        public UINavigationController Content { get; set; }

        public HeaderViewController()
        {
            Content = new UINavigationController();
            Content.NavigationBarHidden = true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.FromRGB(0xE1, 0x00, 0x0E);

            NavigationController.NavigationBarHidden = true;

            AddChildViewController(Content);

            UserInterface.Build(this, View, 
                new Element<UIView>(() => new UIView())
                    .Name("deviceStatus")
                    .Set(deviceStatus => deviceStatus.BackgroundColor = UIColor.FromRGB(0xE1, 0x00, 0x0E))
                    .Constraints(deviceStatus => new[]
                    {
                        deviceStatus.AtTopOfParent().Plus(20),
                        deviceStatus.AtLeftOfParent(),
                        deviceStatus.AtRightOfParent(),
                        deviceStatus.Height().EqualTo(20),
                    }),
                new Element<UIView>(() => new UIView())
                    .Name("navbar")
                    .Set(navbar => navbar.BackgroundColor = UIColor.White)
                    .Constraints((navbar, deviceStatus) => new[]
                    {
                        navbar.Top().EqualTo(0).BottomOf(deviceStatus),
                        navbar.AtLeftOfParent(0),
                        navbar.AtRightOfParent(0),
                        navbar.Height().EqualTo(50),
                    })
                    .Children(
                        new Element<UILabel>(() => new UILabel())
                            .Set(x => x.TintColor = UIColor.Black)
                            .Constraints(title => new FluentLayout[]
                            {
                                title.CenterXOfParent(),
                                title.CenterYOfParent(),
                                title.WidthOfSelf(),
                                title.HeightOfSelf()
                            })
                            .Bindings<RootViewModel>((title, set) => set.Bind(title).To(x => x.Title))
                    ),
                new Element<UIView>(() => Content.View)
                    .Constraints((contentView, navbar) => new[]
                    {
                        Content.View.Top().EqualTo(0).BottomOf(navbar),
                        Content.View.AtLeftOfParent(0),
                        Content.View.AtRightOfParent(0),
                        Content.View.AtBottomOfParent(0)
                    })
                );
        }
    }
}
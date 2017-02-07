using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Apple.InterfaceBuilder;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using Daven.SyntaxExtensions;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    [MvxPanelPresentation(MvxPanelEnum.Right, MvxPanelHintType.ActivePanel, false)]
    public class MenuViewController : MvxViewController<RootViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.FromRGB(0xE1, 0x00, 0x0E);

            var ui = new UserInterface<MenuViewController, RootViewModel>(this, View);

            ui.Build(
                ui.Element(new UITableView())
                    .Set(items => items.Source = new MvxStandardTableViewSource(items, "TitleText Title"))
                    .Constraints(items => new []
                    {
                        items.AtTopOfParent(40),
                        items.AtLeftOfParent(),
                        items.AtRightOfParent(),
                        items.AtBottomOfParent()
                    })
                    .Bindings((items, set) =>
                    {
                        var source = items.Source.CastInstanceTo<MvxStandardTableViewSource>();
                        set.Bind(source).To(x => x.MenuItems);
                        set.Bind(source)
                            .For(x => x.SelectionChangedCommand)
                            .To(x => x.Navigate);
                    })
                );

            ViewModel.NavigateTo(0);
        }
    }
}
using System.Collections.Generic;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Controls;
using Danfoss.CaseControllerApp.Apple.Extensions;
using Danfoss.CaseControllerApp.Apple.InterfaceBuilder;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using Daven.SyntaxExtensions;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public abstract class WizardStepViewController<TController, T> : MvxViewController<T> 
        where T : WizardStepViewModel
        where TController : WizardStepViewController<TController, T>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var ui = new UserInterface<TController, T>((TController)this, View);

            var elements = new List<Element>()
            {
                ui.Element(new UIView(), "title")
                    .Set(title => title.BackgroundColor = UIColor.LightGray)
                    .Constraints(title => new []
                    {
                        title.AtLeftOfParent(),
                        title.AtRightOfParent(),
                        title.AtTopOfParent(),
                        title.Height().EqualTo(50)
                    })
                    .Children(
                        ui.Element(new UILabel())
                            .Set(text => text.TextColor = UIColor.DarkGray)
                            .Constraints(text => new[]
                            {
                                text.CenterXOfParent(),
                                text.CenterYOfParent(),
                                text.WidthOfSelf(),
                                text.HeightOfParent()
                            })
                            .Bindings((text, set) => set.Bind(text).To(vm => vm.Title))
                    )
            };

            elements.AddRange(CreateUI(ui));

            elements.Add(ui.Element(new DanfossPagerView(3, ViewModel.PagedViewId.ParseToInt32()))
                .Constraints((pager, title) => new[]
                {
                    pager.AtRightOfParent(0),
                    pager.Top().EqualTo(0).BottomOf(title),
                    pager.Width().EqualTo(30),
                    pager.HeightOfParent(0)
                }));

            ui.Build(elements.ToArray());
        }

        protected abstract IEnumerable<Element> CreateUI(UserInterface<TController, T> ui);
    }
}
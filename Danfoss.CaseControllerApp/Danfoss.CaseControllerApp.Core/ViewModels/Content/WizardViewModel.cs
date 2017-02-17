using System.Collections.Generic;
using System.Linq;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using Daven.SyntaxExtensions;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels.Content
{
    public class WizardViewModel : ChildViewModel, IMvxPageViewModel
    {
        public override string Title { get; } = "Wizard!!";

        private readonly List<WizardStepViewModel> _steps = new List<WizardStepViewModel>()
        {
            new WizardApplicationTypeViewModel(),
            new WizardCaseTypeViewModel(),
            new WizardFoodTypeViewModel()
        };

        public List<WizardStepViewModel> Steps => _steps;

        public IMvxPagedViewModel GetDefaultViewModel()
        {
            return _steps[0];
        }

        public IMvxPagedViewModel GetNextViewModel(IMvxPagedViewModel currentViewModel)
        {
            var currentIndex = currentViewModel.PagedViewId.ParseToInt32();
            var nextIndex = (currentIndex - 1).ToString();
            return _steps.FirstOrDefault(x => x.PagedViewId == nextIndex);
        }

        public IMvxPagedViewModel GetPreviousViewModel(IMvxPagedViewModel currentViewModel)
        {
            var currentIndex = currentViewModel.PagedViewId.ParseToInt32();
            var prevIndex = (currentIndex + 1).ToString();
            return _steps.FirstOrDefault(x => x.PagedViewId == prevIndex);
        }
    }

    public abstract class WizardStepViewModel : MvxViewModel, IMvxPagedViewModel
    {
        public string PagedViewId { get; }

        public string Title { get; }

        protected WizardStepViewModel(int pagedViewId, string title)
        {
            PagedViewId = pagedViewId.ToString();
            Title = title;
        }
    }

    public class WizardApplicationTypeViewModel : WizardStepViewModel
    {
        public WizardApplicationTypeViewModel() : base(1, "Application Type")
        {
        }

        public string Help { get; set; } = "Some additional property";

        public IMvxCommand ChangeHelp => new MvxCommand(() =>
        {
            Help += "1";
        });
    }

    public class WizardCaseTypeViewModel : WizardStepViewModel
    {
        public WizardCaseTypeViewModel() : base(2, "Case Type")
        {
        }
    }

    public class WizardFoodTypeViewModel : WizardStepViewModel
    {
        public WizardFoodTypeViewModel() : base(3, "Food Type")
        {
        }
    }
}
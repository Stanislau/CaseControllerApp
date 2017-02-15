using System;
using Danfoss.CaseControllerApp.Core.Services.Helpers;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels.Root
{
    public class RootViewModel : MvxViewModel
    {
        public string Title { get; private set; } = "Get your user profile ready!";

        public bool IsBackDisplayed => BackViewModel != null;

        public Type BackViewModel { get; private set; } = null;

        public bool SideNavigationEnabled { get; private set; } = true;

        public IMvxCommand BackCommand => new MvxCommand(() =>
        {
            if (BackViewModel != null)
            {
                ShowViewModel(BackViewModel);
            }
        });

        public RootViewModel(IRootViewModelNotifier notifier)
        {
            notifier.CurrentViewModel.Subscribe(childViewModel =>
            {
                Title = childViewModel.Title;
                BackViewModel = childViewModel.BackViewModel;
                SideNavigationEnabled = childViewModel.SideNavigationEnabled;
            });
        }

        public MvxObservableCollection<MenuItemViewModel> MenuItems { get; } = new MvxObservableCollection<MenuItemViewModel>(new []
        {
            new MenuItemViewModel("First", typeof(FirstViewModel)),
            new MenuItemViewModel("Second", typeof(SecondViewModel)),
            new MenuItemViewModel("Wizard", typeof(WizardViewModel)), 
        });

        public IMvxCommand Navigate => new MvxCommand<MenuItemViewModel>(menuItem => ShowViewModel(menuItem.ViewModelType));

        public void NavigateTo(int position)
        {
            var menuItem = MenuItems[position];

            ShowViewModel(menuItem.ViewModelType);
        }
    }
}
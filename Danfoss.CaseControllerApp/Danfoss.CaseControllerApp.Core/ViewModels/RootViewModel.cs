using System;
using System.Linq;
using Danfoss.CaseControllerApp.Core.Services.Helpers;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels
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

        public MenuItemViewModel[] MenuItems { get; } = new[]
        {
            new MenuItemViewModel("First", typeof(FirstViewModel)),
            new MenuItemViewModel("Second", typeof(SecondViewModel)),
        };

        public void NavigateTo(int position)
        {
            var menuItem = MenuItems[position];

            ShowViewModel(menuItem.ViewModelType);
        }
    }

    public class FirstViewModel : ChildViewModel
    {
        public string Text1 { get; set; } = "First";

        public string Blah { get; set; }

        public override void Start()
        {
            base.Start();

            Blah = "Brand new value!";
        }

        public override string Title { get; } = "First";
    }

    public class SecondViewModel : ChildViewModel
    {
        public string Text2 { get; set; } = "Second";

        public override string Title { get; } = "Second";

        public override Type BackViewModel { get; } = typeof (FirstViewModel);
    }

    public class MenuItemViewModel
    {
        public string Title { get; }

        public Type ViewModelType { get; }

        public MenuItemViewModel(string title, Type viewModelType)
        {
            Title = title;
            ViewModelType = viewModelType;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
using System;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
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
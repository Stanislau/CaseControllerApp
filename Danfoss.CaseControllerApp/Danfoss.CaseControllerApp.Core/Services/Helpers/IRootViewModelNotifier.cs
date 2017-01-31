using System;
using Danfoss.CaseControllerApp.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.Services.Helpers
{
    public interface IRootViewModelNotifier
    {
        IObservable<ChildViewModel> CurrentViewModel { get; }

        void ViewModelChanged(ChildViewModel viewModel);
    }
}
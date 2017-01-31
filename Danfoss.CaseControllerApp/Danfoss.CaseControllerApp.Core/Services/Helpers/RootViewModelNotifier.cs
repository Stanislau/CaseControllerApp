using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Danfoss.CaseControllerApp.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.Services.Helpers
{
    public class RootViewModelNotifier : IRootViewModelNotifier
    {
        public IObservable<ChildViewModel> CurrentViewModel => _currentViewModel.AsObservable();

        private Subject<ChildViewModel> _currentViewModel = new Subject<ChildViewModel>(); 

        public void ViewModelChanged(ChildViewModel viewModel)
        {
            _currentViewModel.OnNext(viewModel);
        }
    }
}
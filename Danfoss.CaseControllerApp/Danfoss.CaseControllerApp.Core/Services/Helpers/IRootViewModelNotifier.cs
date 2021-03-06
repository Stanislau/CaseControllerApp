﻿using System;
using Danfoss.CaseControllerApp.Core.ViewModels;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;

namespace Danfoss.CaseControllerApp.Core.Services.Helpers
{
    public interface IRootViewModelNotifier
    {
        IObservable<ChildViewModel> CurrentViewModel { get; }

        void ViewModelChanged(ChildViewModel viewModel);
    }
}
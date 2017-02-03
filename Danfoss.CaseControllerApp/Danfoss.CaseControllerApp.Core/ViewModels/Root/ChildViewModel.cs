using System;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels.Root
{
    public abstract class ChildViewModel : MvxViewModel
    {
        public abstract string Title { get; }

        public virtual Type BackViewModel { get; } = null;

        public virtual bool SideNavigationEnabled { get; } = true;
    }
}
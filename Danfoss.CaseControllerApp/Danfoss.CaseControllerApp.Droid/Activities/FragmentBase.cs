using Danfoss.CaseControllerApp.Core.Services.Helpers;
using Danfoss.CaseControllerApp.Core.ViewModels;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    public abstract class FragmentBase<T> : MvxFragment<T> where T : ChildViewModel
    {
        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            Mvx.Resolve<IRootViewModelNotifier>().ViewModelChanged(ViewModel);
        }
    }
}
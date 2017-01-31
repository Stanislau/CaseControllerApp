using Android.OS;
using Android.Runtime;
using Android.Views;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [MvxFragment(typeof(RootViewModel), Resource.Id.frameLayout)]
    [Register("danfoss.casecontrollerapp.droid.activities.FirstFragment")]
    public class FirstFragment : FragmentBase<FirstViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.FirstFragment, null);
        }
    }
}
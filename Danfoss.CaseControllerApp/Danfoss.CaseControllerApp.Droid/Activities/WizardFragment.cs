using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using Daven.SyntaxExtensions;
using Java.Lang;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [MvxFragment(typeof(RootViewModel), Resource.Id.frameLayout)]
    [Register("danfoss.casecontrollerapp.droid.activities.WizardFragment")]
    public class WizardFragment : FragmentBase<WizardViewModel>
    {
        private readonly Dictionary<Type, Type> _viewModelToFragment = new Dictionary<Type, Type>()
        {
            { typeof(WizardApplicationTypeViewModel), typeof(WizardApplicationTypeFragment) },
            { typeof(WizardCaseTypeViewModel), typeof(WizardCaseTypeFragment) },
            { typeof(WizardFoodTypeViewModel), typeof(WizardFoodTypeFragment) }
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.Wizard, null);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewPager);
            var adapter = new DanfossPagerAdapter(Context, ChildFragmentManager, ViewModel.Steps.GetReversed().Select(x => new MvxCachingFragmentStatePagerAdapter2.FragmentInfo(x.Title, _viewModelToFragment[x.GetType()], x)));
            viewPager.Adapter = adapter;
            viewPager.SetCurrentItem(ViewModel.Steps.Count - 1, false);

            return view;
        }
    }



    public class DanfossPagerAdapter : MvxCachingFragmentStatePagerAdapter2
    {
        public DanfossPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public DanfossPagerAdapter(Context context, FragmentManager fragmentManager, IEnumerable<FragmentInfo> fragments) : base(context, fragmentManager, fragments)
        {
        }

        //public override void RestoreState(IParcelable state, ClassLoader loader)
        //{
        //    //should be investigated why disabling the restore state works better than enabling

        //    return;

        //    base.RestoreState(state, loader);
        //}
    }
}
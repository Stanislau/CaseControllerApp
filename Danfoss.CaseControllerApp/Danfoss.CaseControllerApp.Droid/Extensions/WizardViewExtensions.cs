using Android.Views;
using Android.Widget;
using Daven.SyntaxExtensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace Danfoss.CaseControllerApp.Droid.Extensions
{
    public static class WizardViewExtensions
    {
        public static View SetWizardContent(this View view, int layoutName, IMvxBindingContextOwner contextOwner)
        {
            var relativeLayout = view.CastInstanceTo<RelativeLayout>();

            var content = contextOwner.BindingInflate(layoutName, null);

            var layoutParams = new RelativeLayout.LayoutParams(0, 0);
            layoutParams.AddRule(LayoutRules.AlignParentLeft);
            layoutParams.AddRule(LayoutRules.AlignParentBottom);
            layoutParams.AddRule(LayoutRules.Below, Resource.Id.title);
            layoutParams.AddRule(LayoutRules.LeftOf, Resource.Id.pager);

            relativeLayout.AddView(content, layoutParams);

            return view;
        }
    }
}
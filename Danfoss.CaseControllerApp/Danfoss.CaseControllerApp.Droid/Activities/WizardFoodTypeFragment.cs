using Android.OS;
using Android.Runtime;
using Android.Views;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using Danfoss.CaseControllerApp.Droid.Extensions;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Register("danfoss.casecontrollerapp.droid.activities.WizardFoodTypeFragment")]
    public class WizardFoodTypeFragment : MvxFragment<WizardFoodTypeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.WizardStep, null).SetWizardContent(Resource.Layout.WizardFoodType, this);
        }
    }
}
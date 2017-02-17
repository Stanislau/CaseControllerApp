using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using Danfoss.CaseControllerApp.Droid.Extensions;
using Daven.SyntaxExtensions;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Register("danfoss.casecontrollerapp.droid.activities.WizardApplicationTypeFragment")]
    public class WizardApplicationTypeFragment : MvxFragment<WizardApplicationTypeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.WizardStep, null).SetWizardContent(Resource.Layout.WizardApplicationType, this);
        }
    }
}
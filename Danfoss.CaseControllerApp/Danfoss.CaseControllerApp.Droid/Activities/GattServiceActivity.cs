using Android.App;
using Android.OS;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Service View")]
    public class GattServiceActivity : MvxActivity<GattServiceViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Service);
        }
    }
}
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Create user", Theme = "@style/AppTheme")]
    public class CreateAccountActivity : MvxAppCompatActivity<CreateAccountViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateAccount);

            var tb = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(tb);
        }
    }
}
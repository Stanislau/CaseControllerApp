using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Create user", Theme = "@style/AppTheme")]
    public class CreateAccountActivity : MvxAppCompatActivity<CreateAccountViewModel>
    {
        private DrawerLayout _drawer;
        private Toolbar _toolbar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateAccount);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            _drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            FindViewById<DrawerArrowView>(Resource.Id.toggleButton1).Sync(_drawer);
        }
    }
}
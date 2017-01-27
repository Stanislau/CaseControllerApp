using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Create user", Theme = "@style/AppTheme")]
    public class CreateAccountActivity : MvxAppCompatActivity<CreateAccountViewModel>
    {
        private DrawerLayout _drawer;
        private Toolbar _toolbar;
        private ActionBarDrawerToggle _actionBarDrawerToggle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.CreateAccount);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);

            _drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            
            _actionBarDrawerToggle = new ActionBarDrawerToggle(this, _drawer, _toolbar, Resource.String.Hello, Resource.String.Hello);
            _drawer.AddDrawerListener(_actionBarDrawerToggle);

            _actionBarDrawerToggle.SyncState();

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _drawer.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
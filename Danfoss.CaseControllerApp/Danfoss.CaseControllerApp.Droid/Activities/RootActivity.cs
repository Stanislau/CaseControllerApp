using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using Danfoss.CaseControllerApp.Core.Services.Helpers;
using Danfoss.CaseControllerApp.Core.ViewModels;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
using Daven.SyntaxExtensions;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Create user", Theme = "@style/AppTheme")]
    public class RootActivity : MvxCachingFragmentCompatActivity<RootViewModel>
    {
        private DrawerLayout _drawer;
        private Toolbar _toolbar;
        private ListView _menuItems;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Root);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            _drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            FindViewById<DrawerArrowView>(Resource.Id.toggleButton1).Sync(_drawer);

            _menuItems = FindViewById<ListView>(Resource.Id.menuItems);
            _menuItems.Adapter = new ArrayAdapter<string>(this, global::Android.Resource.Layout.SimpleListItem1, ViewModel.MenuItems.Select(x => x.Title).ToArray());
            _menuItems.ItemClick += MenuItemsOnItemClick;

            if (SupportFragmentManager.Fragments.IsNullOrEmpty())
            {
                ShowFragmentAt(0);
            }
        }

        public override void OnBackPressed()
        {
            ViewModel.BackCommand.Execute();
        }

        public override void OnFragmentChanged(IMvxCachedFragmentInfo fragmentInfo)
        {
            base.OnFragmentChanged(fragmentInfo);

            Mvx.Resolve<IRootViewModelNotifier>().ViewModelChanged(fragmentInfo.CachedFragment.ViewModel.Cast<ChildViewModel>());
        }

        private void MenuItemsOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            ShowFragmentAt(itemClickEventArgs.Position);
        }

        void ShowFragmentAt(int position)
        {
            ViewModel.NavigateTo(position);

            _drawer.CloseDrawers();
        }
    }
}
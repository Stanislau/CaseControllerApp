using Acr.UserDialogs;
using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Splash")]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
            
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            UserDialogs.Init(this);
        }
    }
}
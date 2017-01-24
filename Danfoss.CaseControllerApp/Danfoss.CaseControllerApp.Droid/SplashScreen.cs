using Acr.UserDialogs;
using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid
{
    [Activity(Label = "Danfoss", MainLauncher = true, Icon = "@drawable/icon")]
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
using Android.App;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid
{
    [Activity(Label = "Danfoss", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
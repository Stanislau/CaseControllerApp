using Android.App;
using Android.OS;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Danfoss.CaseControllerApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);
        }
    }
}


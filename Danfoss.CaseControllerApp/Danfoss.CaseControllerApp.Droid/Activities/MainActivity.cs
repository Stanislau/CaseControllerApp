using Android.App;
using Android.OS;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Hello world view!")]
    public class MainActivity : MvxActivity<HelloWorldViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.HelloWorld);
        }
    }
}


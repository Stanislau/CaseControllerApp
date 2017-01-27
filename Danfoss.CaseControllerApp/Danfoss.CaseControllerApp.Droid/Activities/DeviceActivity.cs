using Android.App;
using Android.OS;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Activity(Label = "Device View")]
    public class DeviceActivity : MvxActivity<DeviceViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Device);
        }
    }

    //[Activity(Label = "Device View")]
    //public class CreateUserActivity : MvxActivity<DeviceViewModel>
    //{
    //    protected override void OnCreate(Bundle bundle)
    //    {
    //        base.OnCreate(bundle);

    //        SetContentView(Resource.Layout.Device);
    //    }
    //}
}
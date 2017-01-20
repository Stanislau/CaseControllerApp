using Android.Content;
using Danfoss.CaseControllerApp.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;

namespace Danfoss.CaseControllerApp.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}
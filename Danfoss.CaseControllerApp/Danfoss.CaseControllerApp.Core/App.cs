using Acr.UserDialogs;
using Danfoss.CaseControllerApp.Core.Services;
using Danfoss.CaseControllerApp.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace Danfoss.CaseControllerApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            Mvx.RegisterSingleton(() => UserDialogs.Instance);

            CreatableTypes()
                .EndingWith("Factory")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton(); 

            Mvx.RegisterSingleton<IBluetoothService2>(() => new BluetoothService2());

            Mvx.RegisterSingleton<IMvxAppStart>(new CustomAppStart());
        }
    }

    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            Mvx.Resolve<IBluetoothService2>().Start();

            ShowViewModel<DeviceListViewModel>();
        }
    }
}
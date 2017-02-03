using Acr.UserDialogs;
using Danfoss.CaseControllerApp.Core.Services;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth;
using Danfoss.CaseControllerApp.Core.Services.Helpers;
using Danfoss.CaseControllerApp.Core.ViewModels;
using Danfoss.CaseControllerApp.Core.ViewModels.Content;
using Danfoss.CaseControllerApp.Core.ViewModels.Root;
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

            Mvx.RegisterSingleton<IBluetoothService>(() => new BluetoothService());

            Mvx.RegisterSingleton<IRootViewModelNotifier>(() => new RootViewModelNotifier());

            Mvx.RegisterSingleton<IMvxAppStart>(new DanfossAppStart());
        }
    }

    public class DanfossAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            ShowViewModel<RootViewModel>();
        }
    }

    public class BleTestAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            Mvx.Resolve<IBluetoothService>().Start();

            ShowViewModel<DeviceListViewModel>();
        }
    }
}
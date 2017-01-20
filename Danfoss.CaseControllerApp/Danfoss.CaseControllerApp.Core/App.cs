using Acr.UserDialogs;
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

            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<HelloWorldViewModel>());
        }
    }
}
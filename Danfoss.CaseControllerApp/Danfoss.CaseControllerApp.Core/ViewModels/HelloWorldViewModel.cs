using System;
using System.Linq;
using System.Reactive.Linq;
using Acr.UserDialogs;
using Danfoss.CaseControllerApp.Core.Services;
using Daven.SyntaxExtensions;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class HelloWorldViewModel : MvxViewModel
    {
        private readonly IBluetoothService3 _service3;

        public HelloWorldViewModel(IBluetoothService3 service3)
        {
            _service3 = service3;

            _service3.ScanCompleted
                .Subscribe((devices) => UserDialogs.Instance.Alert(devices.Select(x => x.AdvertisementData.LocalName).JoinStrings("\n"), "Device List"));

            Cancel = new MvxCommand(() =>
            {
                //_service3.Stop();
            });

            Scan = new MvxCommand(() =>
            {
                _service3.Scan();
            });
        }

        public string Title { get; private set; } = "Hello World!";

        public IMvxCommand Cancel { get; }

        public IMvxCommand Resume { get; }

        public IMvxCommand Scan { get; }

        public override void Start()
        {
            base.Start();
        }
    }
}
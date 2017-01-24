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
        private readonly IBluetoothService _service;

        public HelloWorldViewModel(IBluetoothService service)
        {
            _service = service;

            _service.ScanCompleted
                .Subscribe((devices) => UserDialogs.Instance.Alert(devices.Select(x => x.AdvertisementData.LocalName).JoinStrings("\n"), "Device List"));

            Cancel = new MvxCommand(() =>
            {
                //_service.Stop();
            });

            Scan = new MvxCommand(() =>
            {
                _service.Scan();
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
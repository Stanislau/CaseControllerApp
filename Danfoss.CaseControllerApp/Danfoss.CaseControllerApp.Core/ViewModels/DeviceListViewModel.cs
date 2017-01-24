using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.Services;
using Daven.SyntaxExtensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class DeviceListViewModel : MvxViewModel
    {
        public ObservableCollection<DeviceViewModel> Devices { get; } = new MvxObservableCollection<DeviceViewModel>();

        public IMvxCommand DeviceSelected => new MvxCommand<DeviceViewModel>(device =>
        {
            ShowViewModel<DeviceViewModel>(new GuidParameters() { Uuid = device.Uuid });
        });

        public override void Start()
        {
            base.Start();

            SyncBluetoothWithDevicesCollection();
        }

        private readonly IBluetoothService _service;

        public DeviceListViewModel(IBluetoothService service)
        {
            _service = service;
        }

        private void SyncBluetoothWithDevicesCollection()
        {
            foreach (var scanResult in _service.Devices)
            {
                Devices.Add(Create(scanResult));
            }

            _service.DeviceAdded.Subscribe(device =>
            {
                Devices.Add(Create(device));
            });
        }

        private DeviceViewModel Create(CaseController scanResult) => Mvx.IocConstruct<DeviceViewModel>().Set(scanResult.Device.Uuid);
    }
}
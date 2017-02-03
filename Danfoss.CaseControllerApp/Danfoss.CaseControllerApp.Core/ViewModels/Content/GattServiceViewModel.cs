using System;
using System.Collections.ObjectModel;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Common;
using Danfoss.CaseControllerApp.Core.ViewModels.Parameters;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels.Content
{
    public class GattServiceViewModel : MvxViewModel
    {
        private readonly IBluetoothService _ble;
        private ICaseControllerService _service;

        public GattServiceViewModel(IBluetoothService ble)
        {
            _ble = ble;
        }

        private Guid _deviceUuid;

        public Guid Uuid { get; set; }

        public string Description { get; set; }

        public ObservableCollection<CharacteristicViewModel> Characteristics { get; } = new ObservableCollection<CharacteristicViewModel>();

        public IMvxCommand CharacteristicSelected => new MvxCommand<CharacteristicViewModel>(characteristic =>
        {
            ShowViewModel<CharacteristicViewModel>(new CharacteristicList() { Device = _deviceUuid, Service = Uuid, Characteristic = characteristic.Uuid });
        });

        public bool IsScanning { get; private set; }

        public string ConnectionAction => IsScanning ? "Stop" : "Start";

        public IMvxCommand ToggleConnection => new MvxCommand(() =>
        {
            if (IsScanning)
            {
                _service.StopScan();
                RaisePropertyChanged(nameof(ConnectionAction));
            }
            else
            {
                _service.Scan();
                RaisePropertyChanged(nameof(ConnectionAction));
            }
        });

        public void Init(ServiceLink link)
        {
            Set(link.Device, link.Service);
        }

        public GattServiceViewModel Set(Guid deviceUuid, Guid serviceUuid)
        {
            if (_service == null)
            {
                Uuid = serviceUuid;
                _deviceUuid = deviceUuid;

                _service = _ble.GetDevice(deviceUuid).GetService(serviceUuid);

                Description = _service.Description;

                _service.IsScanning.Subscribe(isScanning => IsScanning = isScanning);

                _service.SyncTo(Characteristics, characteristic => new CharacteristicViewModel(_ble).Set(_deviceUuid, Uuid, characteristic.Uuid));
            }

            return this;
        }
    }
}
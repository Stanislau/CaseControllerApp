using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.Services;
using Danfoss.CaseControllerApp.Core.ViewModels.Parameters;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class GattServiceViewModel : MvxViewModel
    {
        private readonly IBluetoothService _ble;
        private CaseControllerService _service;

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

        public string ConnectionAction => _service.IsScanning ? "Stop" : "Start";

        public IMvxCommand ToggleConnection => new MvxCommand(() =>
        {
            if (_service.IsScanning)
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

                foreach (var characteristic in _service.Characteristics)
                {
                    Characteristics.Add(Create(characteristic.Uuid));
                }

                _service.CharacteristicAdded.Subscribe(c => Characteristics.Add(Create(c.Uuid)));
            }

            return this;
        }

        private CharacteristicViewModel Create(Guid uuid) => new CharacteristicViewModel(_ble).Set(_deviceUuid, Uuid, uuid);
    }
}
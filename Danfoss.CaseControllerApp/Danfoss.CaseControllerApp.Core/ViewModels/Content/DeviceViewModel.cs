using System;
using System.Collections.ObjectModel;
using Acr.Ble;
using Acr.UserDialogs;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Common;
using Danfoss.CaseControllerApp.Core.ViewModels.Parameters;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels.Content
{
    public class DeviceViewModel : MvxViewModel
    {
        private readonly IBluetoothService _service;

        public string Name { get; set; }

        public Guid Uuid { get; private set; }

        public int Rssi { get; set; }

        public ConnectionStatus State { get; set; }

        public ObservableCollection<GattServiceViewModel> GattServices { get; } = new MvxObservableCollection<GattServiceViewModel>();

        public string MacAddress => Uuid.ToString();

        private ICaseController _device;

        public string ConnectionAction => State == ConnectionStatus.Connected ? "Disconnect" : "Connect";

        public IMvxCommand ServiceSelected => new MvxCommand<GattServiceViewModel>(service =>
        {
            ShowViewModel<GattServiceViewModel>(new ServiceLink() { Device = Uuid, Service = service.Uuid });
        });

        public DeviceViewModel(IBluetoothService service)
        {
            _service = service;
        }

        public void Init(DeviceLink parameter)
        {
            Set(parameter.Device);
        }

        public DeviceViewModel Set(Guid uuid)
        {
            if (Uuid.Equals(Guid.Empty) == false && Uuid.Equals(uuid) == false)
            {
                UserDialogs.Instance.Alert("Device override.");
            }

            if (Uuid.Equals(uuid)) return this;

            Uuid = uuid;

            _device = _service.GetDevice(Uuid);

            _device.Name.Subscribe(name => Name = name);

            _device.Rssi.Subscribe(rssi => Rssi = rssi);

            _device.State.Subscribe(state => State = state);

            _device.SyncTo(GattServices, convert: (service) => new GattServiceViewModel(_service).Set(_device.Uuid, service.Uuid));

            return this;
        }

        public IMvxCommand ToggleConnection => new MvxCommand(() =>
        {
            if (State == ConnectionStatus.Connected)
            {
                _device.Disconnect();
            }
            else
            {
                _device.ConnectAndStartScan();
            }
        });

        
    }
}
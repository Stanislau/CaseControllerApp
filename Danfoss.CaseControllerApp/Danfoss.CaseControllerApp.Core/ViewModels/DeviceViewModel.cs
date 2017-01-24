using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Acr.Ble;
using Acr.UserDialogs;
using Danfoss.CaseControllerApp.Core.Services;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels
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

        private CaseController _device;

        public string ConnectionAction => State == ConnectionStatus.Connected ? "Disconnect" : "Connect";

        public IMvxCommand ServiceSelected => new MvxCommand<GattServiceViewModel>(service =>
        {
            ShowViewModel<GattServiceViewModel>(new ServiceParameters() { Device = Uuid, Service = service.Uuid });
        });

        public DeviceViewModel(IBluetoothService service)
        {
            _service = service;
        }

        public void Init(GuidParameters parameter)
        {
            Set(parameter.Uuid);
        }

        public DeviceViewModel Set(Guid uuid)
        {
            if (Uuid.Equals(Guid.Empty) == false && Uuid.Equals(uuid) == false)
            {
                UserDialogs.Instance.Alert("Uuid override.");
            }

            if (Uuid.Equals(uuid)) return this;

            Uuid = uuid;

            _device = _service.GetDevice(Uuid);

            _device.Name.Subscribe(name => Name = name);

            _device.Rssi.Subscribe(rssi => Rssi = rssi);

            _device.State.Subscribe(state => State = state);

            foreach (var service in _device.Services)
            {
                GattServices.Add(CreateGattService(service.Uuid));
            }

            _device.ServiceAdded.Subscribe(service =>
            {
                GattServices.Add(CreateGattService(service.Uuid));
                Debug.WriteLine("Added: " + service.Uuid);
            });

            _device.ServicesCleared.Subscribe(empty => GattServices.Clear());

            return this;
        }

        private GattServiceViewModel CreateGattService(Guid uuid) => new GattServiceViewModel(_service).Set(_device.Uuid, uuid);

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
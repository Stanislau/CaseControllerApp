using System;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;
using Danfoss.CaseControllerApp.Core.ViewModels.Parameters;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class CharacteristicViewModel : MvxViewModel
    {
        private readonly IBluetoothService _ble;
        private ICaseControllerCharacteristic _characteristic;

        public Guid Uuid { get; private set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public CharacteristicViewModel(IBluetoothService ble)
        {
            _ble = ble;
        }

        public void Init(CharacteristicList list)
        {
            Set(list.Device, list.Service, list.Characteristic);
        }

        public CharacteristicViewModel Set(Guid device, Guid service, Guid characteristic)
        {
            if (_characteristic == null)
            {
                Uuid = characteristic;

                _characteristic = _ble.GetDevice(device).GetService(service).GetCharacteristic(characteristic);

                Description = _characteristic.Description;

                _characteristic.Value.Subscribe(value => Value = value);
            }

            return this;
        }

        public IMvxCommand Download => new MvxCommand(() => _characteristic.Download().Subscribe(result => Value = result));

        public IMvxCommand Add42 => new MvxCommand(() => _characteristic.Upload(Value + "42"));

        public IMvxCommand Subscribe => new MvxCommand(() => _characteristic.ListenToNotifications());
    }
}
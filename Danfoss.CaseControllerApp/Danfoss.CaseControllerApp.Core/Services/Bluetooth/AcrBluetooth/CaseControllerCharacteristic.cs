using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;
using Acr.UserDialogs;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;
using Daven.SyntaxExtensions;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth
{
    public class CaseControllerCharacteristic : ICaseControllerCharacteristic
    {
        private readonly IGattCharacteristic _characteristic;
        private readonly CaseControllerService _service;

        public Guid Uuid => _characteristic.Uuid;
        public string Description => _characteristic.Description;
        public IObservable<string> Value => _value.AsObservable();

        private readonly BehaviorSubject<string> _value; 

        public CaseControllerCharacteristic(IGattCharacteristic characteristic, CaseControllerService service)
        {
            _characteristic = characteristic;
            _service = service;

            _value = new BehaviorSubject<string>(FromBytes(characteristic.Value));
        }

        public IObservable<string> Download()
        {
            return _characteristic.Read().Select(x => FromBytes(x.Data));
        }

        private string FromBytes(byte[] arr)
        {
            if (arr == null || arr.IsEmpty()) return "EMPTY";

            return new string(arr.Select(Convert.ToChar).ToArray());
        }

        private byte[] ToBytes(string str)
        {
            return str.ToCharArray().Select(Convert.ToByte).ToArray();
        }

        public void Upload(string newValue)
        {
            _characteristic.WriteWithoutResponse(ToBytes(newValue));
        }

        public void ListenToNotifications()
        {
            _characteristic.SubscribeToNotifications().Select(result => FromBytes(result.Data)).Subscribe(result => _value.OnNext(result));
        }
    }
}
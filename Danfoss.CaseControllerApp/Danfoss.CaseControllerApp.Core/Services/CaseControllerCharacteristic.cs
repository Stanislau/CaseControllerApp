using System;
using System.Linq;
using System.Reactive.Linq;
using Acr.Ble;
using Daven.SyntaxExtensions;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class CaseControllerCharacteristic
    {
        private readonly IGattCharacteristic _characteristic;
        private readonly CaseControllerService _service;

        public Guid Uuid => _characteristic.Uuid;
        public string Description => _characteristic.Description;
        public string Value => FromBytes(_characteristic.Value);

        public CaseControllerCharacteristic(IGattCharacteristic characteristic, CaseControllerService service)
        {
            _characteristic = characteristic;
            _service = service;
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

        public IObservable<string> ValueChanged()
        {
            return _characteristic.SubscribeToNotifications().Select(result => FromBytes(result.Data));
        }
    }
}
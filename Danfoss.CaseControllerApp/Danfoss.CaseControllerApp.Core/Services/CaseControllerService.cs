using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;
using Daven.SyntaxExtensions;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class CaseControllerService
    {
        public Guid Uuid => _service.Uuid;

        public string Description => _service.Description;

        public IList<CaseControllerCharacteristic> Characteristics { get; } = new List<CaseControllerCharacteristic>();

        public IObservable<CaseControllerCharacteristic> CharacteristicAdded => _characteristicAdded.AsObservable();
        public bool IsScanning { get; set; }

        private Subject<CaseControllerCharacteristic> _characteristicAdded = new Subject<CaseControllerCharacteristic>();

        private IGattService _service;
        private readonly CaseController _caseController;

        public CaseControllerService(IGattService service, CaseController caseController)
        {
            _service = service;
            _caseController = caseController;
        }

        public void SetNew(IGattService gattService)
        {
            _service = gattService;
        }

        private IDisposable _scan;

        public void Scan()
        {
            StopScan();
            Characteristics.Clear();
            IsScanning = true;
            _scan = _service.WhenCharacteristicDiscovered().Subscribe(chatacteristic =>
            {
                var c = new CaseControllerCharacteristic(chatacteristic, this);
                Characteristics.Add(c);
                _characteristicAdded.OnNext(c);
            });
        }

        public void StopScan()
        {
            IsScanning = false;
            _scan?.Dispose();
        }

        public CaseControllerCharacteristic GetCharacteristic(Guid uuid)
        {
            return Characteristics.FirstOrDefault(x => x.Uuid == uuid);
        }
    }

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
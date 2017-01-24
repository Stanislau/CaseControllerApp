using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;

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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth
{
    public class CaseControllerService : ICaseControllerService
    {
        public Guid Uuid => _service.Uuid;

        public string Description => _service.Description;

        public IObservable<object> Cleared { get; } = new Subject<object>();

        public IEnumerable<CaseControllerCharacteristic> Items => _items;

        public IObservable<CaseControllerCharacteristic> ItemAdded => _characteristicAdded.AsObservable();

        public IObservable<bool> IsScanning => _isScanning;

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
        private readonly IList<CaseControllerCharacteristic> _items = new List<CaseControllerCharacteristic>();
        private BehaviorSubject<bool> _isScanning = new BehaviorSubject<bool>(false);

        public void Scan()
        {
            StopScan();
            _items.Clear();
            _isScanning.OnNext(true);
            _scan = _service.WhenCharacteristicDiscovered().Subscribe(chatacteristic =>
            {
                var c = new CaseControllerCharacteristic(chatacteristic, this);
                _items.Add(c);
                _characteristicAdded.OnNext(c);
            });
        }

        public void StopScan()
        {
            _isScanning.OnNext(false);
            _scan?.Dispose();
        }

        public ICaseControllerCharacteristic GetCharacteristic(Guid uuid)
        {
            return Items.FirstOrDefault(x => x.Uuid == uuid);
        }
    }
}
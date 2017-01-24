using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth
{
    public class BluetoothService : IBluetoothService
    {
        public IObservable<CaseController> ItemAdded => _deviceAdded.AsObservable();
        public IEnumerable<CaseController> Items => _items;
        public IObservable<object> Cleared => _cleared.AsObservable();

        private readonly IList<CaseController> _items = new List<CaseController>();
        private readonly Subject<CaseController> _deviceAdded = new Subject<CaseController>();
        private readonly Subject<object> _cleared = new Subject<object>();

        private IDisposable _scan;

        public void Start()
        {
            _scan = BleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                var existed = Items.FirstOrDefault(x => x.Uuid == scanResult.Device.Uuid);
                if (existed == null)
                {
                    var caseController = new CaseController(scanResult, this);
                    _items.Add(caseController);
                    _deviceAdded.OnNext(caseController);
                }
                else
                {
                    existed.SetNew(scanResult);
                }
            });
        }

        public void Stop()
        {
            _scan.Dispose();
        }

        public ICaseController GetDevice(Guid uuid)
        {
            return Items.First(x => x.Uuid == uuid);
        }
    }
}
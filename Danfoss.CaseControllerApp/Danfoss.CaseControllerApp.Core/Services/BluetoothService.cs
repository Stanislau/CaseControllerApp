using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class BluetoothService : IBluetoothService
    {
        public IObservable<CaseController> Added => _deviceAdded.AsObservable();

        private readonly Subject<CaseController> _deviceAdded = new Subject<CaseController>();

        public IEnumerable<CaseController> Items => _items;

        private readonly IList<CaseController> _items = new List<CaseController>();

        public IObservable<object> Cleared => _cleared.AsObservable();

        private readonly Subject<object> _cleared = new Subject<object>();

        private IDisposable _scan;

        public void Start()
        {
            _scan = BleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                var existed = Items.FirstOrDefault(x => x.Device.Uuid == scanResult.Device.Uuid);
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

        public CaseController GetDevice(Guid uuid)
        {
            return Items.First(x => x.Device.Uuid == uuid);
        }

        
    }
}
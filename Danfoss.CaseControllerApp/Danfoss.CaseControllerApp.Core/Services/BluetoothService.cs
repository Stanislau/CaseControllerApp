using System;
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
        public IObservable<CaseController> DeviceAdded => _deviceAdded.AsObservable();

        private Subject<CaseController> _deviceAdded = new Subject<CaseController>();

        public ObservableCollection<CaseController> Devices { get; } = new ObservableCollection<CaseController>();

        private IDisposable _scan;

        public void Start()
        {
            _scan = BleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                var existed = Devices.FirstOrDefault(x => x.Device.Uuid == scanResult.Device.Uuid);
                if (existed == null)
                {
                    var caseController = new CaseController(scanResult, this);
                    Devices.Add(caseController);
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
            return Devices.First(x => x.Device.Uuid == uuid);
        }

        public IObservable<IGattService> StartDiscoveringServices(Guid deviceId)
        {
            return GetDevice(deviceId).Device.WhenServiceDiscovered();
        }
    }
}
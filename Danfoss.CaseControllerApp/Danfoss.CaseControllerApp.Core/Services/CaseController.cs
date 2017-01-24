using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class CaseController
    {
        public IDevice Device { get; }

        public Guid Uuid => Device.Uuid;

        public IObservable<int> Rssi => _rssi.AsObservable();

        public IObservable<ConnectionStatus> State => _state.AsObservable();

        public IObservable<string> Name => _name.AsObservable();

        public ObservableCollection<CaseControllerService> Services { get; } = new ObservableCollection<CaseControllerService>();

        public IObservable<CaseControllerService> ServiceAdded => _serviceAdded.AsObservable();

        public IObservable<object> ServicesCleared => _servicesCleared.AsObservable();

        private Subject<object> _servicesCleared = new Subject<object>(); 
        
        private Subject<CaseControllerService> _serviceAdded = new Subject<CaseControllerService>(); 

        private BehaviorSubject<string> _name; 

        private BehaviorSubject<ConnectionStatus> _state; 

        private BehaviorSubject<int> _rssi;

        private IDisposable _rssiNative;
        private IDisposable _stateNative;
        private IDisposable _scanNative;

        private readonly IBluetoothService _ble;

        public CaseController(IScanResult scanResult, IBluetoothService ble)
        {
            _ble = ble;
            Device = scanResult.Device;

            _rssi = new BehaviorSubject<int>(scanResult.Rssi);
            _state = new BehaviorSubject<ConnectionStatus>(Device.Status);
            _name = new BehaviorSubject<string>(scanResult.Device.Name);

            scanResult.Device.WhenNameUpdated().Subscribe(newName => _name.OnNext(newName));

            _stateNative = Device.WhenStatusChanged().Subscribe(newStatus =>
            {
                _state.OnNext(newStatus);

                if (newStatus == ConnectionStatus.Connected)
                {
                    _ble.Stop();

                    _rssiNative?.Dispose();

                    _rssiNative = Device.WhenRssiUpdated(TimeSpan.FromSeconds(1)).Subscribe(newRssi => _rssi.OnNext(newRssi));
                }
                else
                {
                    _rssiNative?.Dispose();
                    _rssiNative = null;
                }
            });
        }

        public void SetNew(IScanResult scanResult)
        {
            _rssi.OnNext(scanResult.Rssi);
            _state.OnNext(scanResult.Device.Status);
        }

        private void StartScan()
        {
            _scanNative?.Dispose();

            _scanNative = Device.WhenServiceDiscovered().Subscribe(gattService =>
            {
                var existed = Services.FirstOrDefault(x => x.Uuid == gattService.Uuid);
                if (existed == null)
                {
                    var service = new CaseControllerService(gattService, this);
                    Services.Add(service);
                    _serviceAdded.OnNext(service);
                    Debug.WriteLine("Service OnNext " + service.Uuid);
                }
                else
                {
                    existed.SetNew(gattService);
                }
            });
        }

        public void Disconnect()
        {
            //_ble.Start();

            _scanNative?.Dispose();

            Services.Clear();
            _servicesCleared.OnNext(null);

            Device.Disconnect();
        }

        public void ConnectAndStartScan()
        {
            //_ble.Stop();

            Device.Connect().Subscribe((connection) =>
            {
                StartScan();
            });
        }

        public CaseControllerService GetService(Guid uuid)
        {
            return Services.FirstOrDefault(x => x.Uuid == uuid);
        }
    }
}
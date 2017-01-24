using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;
using Acr.UserDialogs;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth
{
    public class CaseController : ICaseController
    {
        public Guid Uuid => _device.Uuid;
        public IObservable<int> Rssi => _rssi.AsObservable();
        public IObservable<ConnectionStatus> State => _state.AsObservable();
        public IObservable<string> Name => _name.AsObservable();

        public IEnumerable<CaseControllerService> Items => _items;
        public IObservable<CaseControllerService> ItemAdded => _serviceAdded.AsObservable();
        public IObservable<object> Cleared => _servicesCleared.AsObservable();

        private readonly IList<CaseControllerService> _items = new List<CaseControllerService>();
        private readonly Subject<object> _servicesCleared = new Subject<object>(); 
        private readonly Subject<CaseControllerService> _serviceAdded = new Subject<CaseControllerService>(); 
        private readonly BehaviorSubject<string> _name; 
        private readonly BehaviorSubject<ConnectionStatus> _state; 
        private readonly BehaviorSubject<int> _rssi;

        private IDisposable _rssiNative;
        private IDisposable _scanNative;

        private readonly IBluetoothService _ble;

        private readonly IDevice _device;

        public CaseController(IScanResult scanResult, IBluetoothService ble)
        {
            _ble = ble;
            _device = scanResult.Device;

            _rssi = new BehaviorSubject<int>(scanResult.Rssi);
            _state = new BehaviorSubject<ConnectionStatus>(_device.Status);
            _name = new BehaviorSubject<string>(scanResult.Device.Name);

            scanResult.Device.WhenNameUpdated().Subscribe(newName => _name.OnNext(newName));

            _device.WhenStatusChanged().Subscribe(newStatus =>
            {
                _state.OnNext(newStatus);

                if (newStatus == ConnectionStatus.Connected)
                {
                    _ble.Stop();

                    _rssiNative?.Dispose();

                    _rssiNative = _device.WhenRssiUpdated(TimeSpan.FromSeconds(1)).Subscribe(newRssi => _rssi.OnNext(newRssi));
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

            _scanNative = _device.WhenServiceDiscovered().Subscribe(gattService =>
            {
                var existed = Items.FirstOrDefault(x => x.Uuid == gattService.Uuid);
                if (existed == null)
                {
                    var service = new CaseControllerService(gattService, this);
                    _items.Add(service);
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

            _items.Clear();
            _servicesCleared.OnNext(null);

            _device.Disconnect();
        }

        public void ConnectAndStartScan()
        {
            //_ble.Stop();

            _device.Connect().Subscribe(onNext: (connection) =>
            {
                StartScan();
            }, 
            onCompleted: () => UserDialogs.Instance.Alert("Connection completed."),
            onError: exception => UserDialogs.Instance.Alert("Exception while connect " + exception.ToString()));
        }

        public ICaseControllerService GetService(Guid uuid)
        {
            return Items.FirstOrDefault(x => x.Uuid == uuid);
        }
    }
}
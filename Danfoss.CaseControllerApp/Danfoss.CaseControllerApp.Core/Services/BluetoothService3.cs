using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acr.Ble;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class BluetoothService3 : IBluetoothService3
    {
        public Subject<List<IScanResult>> ScanCompleted { get; } = new Subject<List<IScanResult>>();
        public Subject<string> CharacteristicsRead { get; } = new Subject<string>();

        public void Scan()
        {
            var devices = new List<IScanResult>();
            BleAdapter.Current.Scan()
                .Timeout(TimeSpan.FromSeconds(1))
                .Subscribe(
                    onNext: x =>
                    {
                        devices.Add(x);
                    }, 
                    onError: async exception =>
                    {
                        ScanCompleted.OnNext(devices);
                        var connectable = devices.First();
                        await connectable.Device.Connect();
                        await connectable.Device.PairingRequest();
                        connectable.Device.WhenServiceDiscovered().Subscribe(service =>
                        {
                            service.WhenCharacteristicDiscovered().Subscribe(async characteristic =>
                            {
                                var value = await characteristic.Read();
                                var str = BitConverter.ToString(value.Data);
                                CharacteristicsRead.OnNext(str);
                            });
                        });
                    });

            
        }
    }
}
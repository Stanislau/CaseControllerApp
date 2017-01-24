using System;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract
{
    public interface ICaseController : ISyncable<CaseControllerService>
    {
        Guid Uuid { get; }
        IObservable<int> Rssi { get; }
        IObservable<ConnectionStatus> State { get; }
        IObservable<string> Name { get; }

        void Disconnect();
        void ConnectAndStartScan();

        ICaseControllerService GetService(Guid uuid);
    }
}
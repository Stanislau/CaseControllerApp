using System;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract
{
    public interface ICaseControllerService : ISyncable<CaseControllerCharacteristic>
    {
        Guid Uuid { get; }
        string Description { get; }

        ICaseControllerCharacteristic GetCharacteristic(Guid uuid);

        IObservable<bool> IsScanning { get; }
        void StopScan();
        void Scan();
    }
}
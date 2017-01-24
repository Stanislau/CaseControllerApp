using System;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.AcrBluetooth;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract
{
    public interface IBluetoothService : ISyncable<CaseController>
    {
        void Start();

        void Stop();

        ICaseController GetDevice(Guid uuid);
    }
}
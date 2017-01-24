using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public interface IBluetoothService : ISyncable<CaseController>
    {
        void Start();

        void Stop();

        CaseController GetDevice(Guid uuid);
    }
}
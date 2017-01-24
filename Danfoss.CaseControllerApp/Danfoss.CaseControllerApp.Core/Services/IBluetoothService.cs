using System.Collections.Generic;
using System.Reactive.Subjects;
using Acr.Ble;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public interface IBluetoothService
    {
        void Scan();

        Subject<List<IScanResult>> ScanCompleted { get; }
    }
}
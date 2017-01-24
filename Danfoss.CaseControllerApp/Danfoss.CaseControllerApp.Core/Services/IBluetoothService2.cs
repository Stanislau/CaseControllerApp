using System;
using System.Collections.ObjectModel;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public interface IBluetoothService2
    {
        IObservable<CaseController> DeviceAdded { get; }
            
        ObservableCollection<CaseController> Devices { get; }

        void Start();

        void Stop();

        CaseController GetDevice(Guid uuid);

        IObservable<IGattService> StartDiscoveringServices(Guid deviceId);
    }
}
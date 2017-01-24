using System;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract
{
    public interface ICaseControllerCharacteristic
    {
        Guid Uuid { get; }
        string Description { get; }
        IObservable<string> Value { get; }

        IObservable<string> Download();
        void Upload(string newValue);

        void ListenToNotifications();
    }
}
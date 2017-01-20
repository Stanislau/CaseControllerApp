using System;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public interface IBluetoothService
    {
        IObservable<int> IndexChanged { get; }

        void Start();

        void Resume();

        void Stop();
    }
}
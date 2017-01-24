using System;
using System.Collections.Generic;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract
{
    public interface ISyncable<out T>
    {
        IObservable<T> ItemAdded { get; }

        IObservable<object> Cleared { get; } 

        IEnumerable<T> Items { get; }
    }
}
using System;
using System.Collections.Generic;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public interface ISyncable<out T>
    {
        IObservable<T> Added { get; }
        IObservable<object> Cleared { get; } 
        IEnumerable<T> Items { get; }
    }
}
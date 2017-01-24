using System;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class EventBatch : IDisposable
    {
        private readonly IDisposable[] _events;

        public EventBatch(params IDisposable[] events)
        {
            _events = events;
        }

        public void Dispose()
        {
            foreach (var disposable in _events)
            {
                disposable.Dispose();
            }
        }
    }
}
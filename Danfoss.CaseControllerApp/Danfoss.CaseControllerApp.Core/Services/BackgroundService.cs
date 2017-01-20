using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class BackgroundService : IBackgroundService
    {
        public IObservable<int> IndexChanged => _indexChanged.AsObservable();

        private Subject<int> _indexChanged = new Subject<int>();

        private int _index = 0;

        private Task _work;
        private CancellationTokenSource _token = new CancellationTokenSource();

        public void Start()
        {
            _index = 0;
            _work = _work ?? Task.Run(Action);
        }

        async Task Action()
        {
            while (_token.Token.IsCancellationRequested == false)
            {
                await Task.Delay(1000, _token.Token);
                _index++;
                _indexChanged.OnNext(_index);
            }

            _work = null;
            _token = new CancellationTokenSource();
        }

        public void Resume()
        {
            _token = new CancellationTokenSource();
            _work = Task.Run(Action);
        }

        public void Stop()
        {
            _token.Cancel();
        }
    }
}
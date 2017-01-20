using System;
using Danfoss.CaseControllerApp.Core.Services;
using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class HelloWorldViewModel : MvxViewModel
    {
        private readonly IBluetoothService _service;

        public HelloWorldViewModel(IBluetoothService service)
        {
            _service = service;
            _service.IndexChanged.Subscribe(index => Title = index.ToString());
            Cancel = new MvxCommand(() =>
            {
                _service.Stop();
            });
            Resume = new MvxCommand(() =>
            {
                _service.Resume();
            });
        }

        public string Title { get; private set; } = "Hello World!";

        public IMvxCommand Cancel { get; }

        public IMvxCommand Resume { get; }

        public override void Start()
        {
            base.Start();

            _service.Start();
        }
    }
}
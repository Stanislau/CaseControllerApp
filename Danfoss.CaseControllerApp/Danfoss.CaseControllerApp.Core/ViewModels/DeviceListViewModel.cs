using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Acr.Ble;
using Danfoss.CaseControllerApp.Core.Services;
using Danfoss.CaseControllerApp.Core.ViewModels.Parameters;
using Daven.SyntaxExtensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class DeviceListViewModel : MvxViewModel
    {
        public ObservableCollection<DeviceViewModel> Devices { get; } = new MvxObservableCollection<DeviceViewModel>();

        public IMvxCommand DeviceSelected => new MvxCommand<DeviceViewModel>(device =>
        {
            ShowViewModel<DeviceViewModel>(new DeviceLink() { Device = device.Uuid });
        });

        public override void Start()
        {
            base.Start();

            _service.SyncTo(Devices, convert: caseController => Mvx.IocConstruct<DeviceViewModel>().Set(caseController.Uuid));
        }

        private readonly IBluetoothService _service;

        public DeviceListViewModel(IBluetoothService service)
        {
            _service = service;
        }
    }
}
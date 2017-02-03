using System.Collections.ObjectModel;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Common;
using Danfoss.CaseControllerApp.Core.ViewModels.Parameters;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Danfoss.CaseControllerApp.Core.ViewModels.Content
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
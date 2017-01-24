using System;
using Danfoss.CaseControllerApp.Core.Services;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class GattServiceViewModel
    {
        private readonly CaseControllerService _service;

        public GattServiceViewModel(CaseControllerService service)
        {
            _service = service;

            Uuid = service.Uuid;
            Description = service.Description;
        }

        public Guid Uuid { get; set; }

        public string Description { get; set; }
    }
}
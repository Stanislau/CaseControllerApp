using System;
using Acr.Ble;

namespace Danfoss.CaseControllerApp.Core.Services
{
    public class CaseControllerService
    {
        public Guid Uuid => _service.Uuid;

        public string Description => _service.Description;

        private IGattService _service;
        private readonly CaseController _caseController;

        public CaseControllerService(IGattService service, CaseController caseController)
        {
            _service = service;
            _caseController = caseController;
        }

        public void SetNew(IGattService gattService)
        {
            _service = gattService;
        }
    }
}
using System;

namespace Danfoss.CaseControllerApp.Core.ViewModels.Parameters
{
    public class CharacteristicList
    {
        public Guid Device { get; set; }

        public Guid Service { get; set; }

        public Guid Characteristic { get; set; }
    }
}
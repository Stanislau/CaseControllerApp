using System;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class CharacteristicParameters
    {
        public Guid Device { get; set; }

        public Guid Service { get; set; }

        public Guid Characteristic { get; set; }
    }
}
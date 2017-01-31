using System;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class SecondViewModel : ChildViewModel
    {
        public string Text2 { get; set; } = "Second";

        public override string Title { get; } = "Second";

        public override Type BackViewModel { get; } = typeof (FirstViewModel);

        public override bool SideNavigationEnabled { get; } = false;
    }
}
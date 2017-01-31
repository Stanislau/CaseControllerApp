namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class FirstViewModel : ChildViewModel
    {
        public string Text1 { get; set; } = "First";

        public string Blah { get; set; }

        public override void Start()
        {
            base.Start();

            Blah = "Brand new value!";
        }

        public override string Title { get; } = "First";
    }
}
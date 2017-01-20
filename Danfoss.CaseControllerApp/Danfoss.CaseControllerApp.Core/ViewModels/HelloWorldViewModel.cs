using MvvmCross.Core.ViewModels;

namespace Danfoss.CaseControllerApp.Core.ViewModels
{
    public class HelloWorldViewModel : MvxViewModel
    {
        public string Title { get; } = "Hello World!";
    }
}
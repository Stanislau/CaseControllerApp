using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;

namespace Danfoss.CaseControllerApp.Apple.Extensions
{
    public static class MvvmCrossExtensions
    {
        public static void AddFluentBindings<T1, T2>(this T1 controller, Action<MvxFluentBindingDescriptionSet<T1, T2>> setBindings)
            where T2 : MvxViewModel
            where T1 : class, IMvxBindingContextOwner
        {
            var set = controller.CreateBindingSet<T1, T2>();

            setBindings(set);

            set.Apply();
        }
    }
}
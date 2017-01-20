using System;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Extensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;

namespace Danfoss.CaseControllerApp.Apple.Controllers
{
    public class BaseViewController<T> : MvxViewController<T> where T: MvxViewModel
    {
        public void AddFluentConstraints(params FluentLayout[] layouts)
        {
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddFluentConstraints(layouts);
        }

        public void AddFluentBindings(Action<MvxFluentBindingDescriptionSet<BaseViewController<T>, T>> setBindings)
        {
            var set = this.CreateBindingSet<BaseViewController<T>, T>();

            setBindings(set);

            set.Apply();
        }
    }
}
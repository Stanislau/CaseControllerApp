using System;
using System.Collections.Generic;
using Danfoss.CaseControllerApp.Apple.Controllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.InterfaceBuilder
{
    public class UserInterface<TViewController, TViewModel>
        where TViewModel : MvxViewModel
        where TViewController : class, IMvxBindingContextOwner
    {
        private readonly TViewController _controller;
        private readonly UIView _view;

        public UserInterface(TViewController controller, UIView view)
        {
            _controller = controller;
            _view = view;
        }

        public Element<T, TViewController, TViewModel> Element<T>(string name = null) where T : UIView
        {
            return new Element<T, TViewController, TViewModel>(name);
        }

        public Element<T, TViewController, TViewModel> Element<T>(T instance, string name = null) where T : UIView
        {
            return new Element<T, TViewController, TViewModel>(instance, name);
        }

        public void Build(params Element[] elements) 
        {
            var items = new Dictionary<string, UIView>();

            foreach (var element in elements)
            {
                element.BuildViewHierarchy(_view, items);
            }

            foreach (var element in elements)
            {
                element.ApplyConstraints(_view, items);
            }

            var set = _controller.CreateBindingSet<TViewController, TViewModel>();
            foreach (var element in elements)
            {
                element.SetBindings(set);
            }
            set.Apply();
        }
    }
}
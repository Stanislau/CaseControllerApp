using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Controllers;
using Daven.SyntaxExtensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.InterfaceBuilder
{
    public class UserInterface
    {
        public static void Build<TViewModel>(MvxViewController<TViewModel> controller, UIView root, params Element[] elements) 
            where TViewModel : MvxViewModel
        {
            var items = new Dictionary<string, UIView>();

            foreach (var element in elements)
            {
                element.BuildViewHierarchy(root, items);
            }

            foreach (var element in elements)
            {
                element.ApplyConstraints(root, items);
            }

            var set = controller.CreateBindingSet<MvxViewController<TViewModel>, TViewModel>();
            foreach (var element in elements)
            {
                element.SetBindings(set);
            }
            set.Apply();
        }
    }

    public abstract class Element
    {
        public UIView View { get; set; }
        public string InternalName { get; set; }

        internal Func<UIView, FluentLayout[]> getConstraints;
        internal Expression<Func<UIView, UIView, FluentLayout[]>> getConstraintsWithDependency1;

        protected Element[] children;

        protected object setBindingsInternal;

        public void ApplyConstraints(UIView parent, Dictionary<string, UIView> items)
        {
            if (getConstraints != null)
            {
                var constraints = getConstraints(View);
                parent.AddConstraints(constraints);
            }
            else if (getConstraintsWithDependency1 != null)
            {
                var dependencyName = getConstraintsWithDependency1.Parameters[1].Name;
                var dependencyItem = items[dependencyName];
                var compiled = getConstraintsWithDependency1.Compile();
                var constraints = compiled(View, dependencyItem);
                parent.AddConstraints(constraints);
            }

            if (children != null && children.Length > 0)
            {
                foreach (var element in children)
                {
                    element.ApplyConstraints(View, items);
                }
            }
        }

        public void SetBindings<TViewModel>(MvxFluentBindingDescriptionSet<MvxViewController<TViewModel>, TViewModel> set) where TViewModel : MvxViewModel
        {
            if (setBindingsInternal != null)
            {
                SetBindingsStrognlyTyped(set);
            }

            if (children != null && children.Length > 0)
            {
                foreach (var element in children)
                {
                    element.SetBindings(set);
                }
            }
        }

        public abstract void SetBindingsStrognlyTyped<TViewModel>(MvxFluentBindingDescriptionSet<MvxViewController<TViewModel>, TViewModel> set) where TViewModel : MvxViewModel;

        public abstract void BuildViewHierarchy(UIView parent, Dictionary<string, UIView> items);
    }

    public class Element<T> : Element
        where T : UIView
    {
        public Element(Func<T> create = null)
        {
            View = create == null ? Activator.CreateInstance<T>() : create();
        }

        public Element<T> Name(string name)
        {
            InternalName = name;
            return this;
        }  

        public Element<T> Set(Action<T> set)
        {
            set(View.CastInstanceTo<T>());
            return this;
        }

        public Element<T> Constraints(Func<UIView, FluentLayout[]> constraints)
        {
            getConstraints = constraints;
            return this;
        }

        public Element<T> Constraints(Expression<Func<UIView, UIView, FluentLayout[]>> constraints)
        {
            getConstraintsWithDependency1 = constraints;
            return this;
        }

        public Element<T> Bindings<TViewModel>(Action<T, MvxFluentBindingDescriptionSet<MvxViewController<TViewModel>, TViewModel>> setBindings) where TViewModel : MvxViewModel
        {
            setBindingsInternal = (object)setBindings;
            return this;
        }  

        public Element<T> Children(params Element[] children)
        {
            base.children = children;
            return this;
        }

        public override void SetBindingsStrognlyTyped<TViewModel>(MvxFluentBindingDescriptionSet<MvxViewController<TViewModel>, TViewModel> set)
        {
            var setBinding = (Action<T, MvxFluentBindingDescriptionSet<MvxViewController <TViewModel>, TViewModel >>)setBindingsInternal;
            setBinding(View.CastInstanceTo<T>(), set);
        }

        public override void BuildViewHierarchy(UIView parent, Dictionary<string, UIView> items)
        {
            if (InternalName != null)
            {
                items[InternalName] = View;
            }

            View.TranslatesAutoresizingMaskIntoConstraints = false;

            parent.Add(View);

            if (base.children != null && base.children.Length > 0)
            {
                foreach (var element in base.children)
                {
                    element.BuildViewHierarchy(View, items);
                }
            }
        }
    }
}
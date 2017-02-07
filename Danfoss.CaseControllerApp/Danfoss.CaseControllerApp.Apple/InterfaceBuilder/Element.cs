using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cirrious.FluentLayouts.Touch;
using Daven.SyntaxExtensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.InterfaceBuilder
{
    public class Element<T,TViewController, TViewModel> : Element
        where T : UIView
        where TViewModel : MvxViewModel
        where TViewController : class, IMvxBindingContextOwner
    {
        public Element(string name = null)
        {
            View = Activator.CreateInstance<T>();
            InternalName = name;
        }

        public Element(T instance, string name = null)
        {
            View = instance;
            InternalName = name;
        }

        public Element<T, TViewController, TViewModel> Set(Action<T> set)
        {
            set(View.CastInstanceTo<T>());
            return this;
        }

        public Element<T, TViewController, TViewModel> Constraints(Func<UIView, FluentLayout[]> constraints)
        {
            getConstraints = constraints;
            return this;
        }

        public Element<T, TViewController, TViewModel> Constraints(Expression<Func<UIView, UIView, FluentLayout[]>> constraints)
        {
            getConstraintsWithDependency1 = constraints;
            return this;
        }

        public Element<T, TViewController, TViewModel> Bindings(Action<T, MvxFluentBindingDescriptionSet<TViewController, TViewModel>> setBindings)
        {
            setBindingsInternal = (object)setBindings;
            return this;
        }  

        public Element<T, TViewController, TViewModel> Children(params Element[] children)
        {
            this.children = children;
            return this;
        }

        public override void SetBindingsStrognlyTyped<TViewController2, TViewModel2>(MvxFluentBindingDescriptionSet<TViewController2, TViewModel2> set)
        {
            var setBinding = (Action<T, MvxFluentBindingDescriptionSet<TViewController2, TViewModel2>>)setBindingsInternal;
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

        public void SetBindings<TViewController, TViewModel>(MvxFluentBindingDescriptionSet<TViewController, TViewModel> set) where TViewModel : MvxViewModel where TViewController : class, IMvxBindingContextOwner
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

        public abstract void SetBindingsStrognlyTyped<TViewController2, TViewModel2>(MvxFluentBindingDescriptionSet<TViewController2, TViewModel2> set) where TViewModel2 : MvxViewModel
            where TViewController2 : class, IMvxBindingContextOwner;

        public abstract void BuildViewHierarchy(UIView parent, Dictionary<string, UIView> items);
    }
}
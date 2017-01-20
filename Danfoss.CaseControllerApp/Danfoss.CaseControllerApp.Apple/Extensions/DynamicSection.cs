using System;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Extensions
{
    public class DynamicSection
    {
        internal Func<FluentLayout[]> getConstraints;
        internal Func<UIView, FluentLayout[]> getConstraintsWithDependent;
        internal Func<FluentLayout[]> whenLast;
        internal Action onBeforeShow;
        internal Action onShown;

        internal UIView[] targets;

        internal Func<bool> isApplicable;
        internal UIView sectionTarget;

        public DynamicSection(UIView target = null, Func<bool> condition = null)
        {
            isApplicable = condition ?? (() => true);
            sectionTarget = target;
        }

        public DynamicSection Constraints(Func<FluentLayout[]> constraints)
        {
            getConstraints = constraints;
            return this;
        }

        public DynamicSection Constraints(Func<UIView, FluentLayout[]> constraints)
        {
            getConstraintsWithDependent = constraints;
            return this;
        }

        public DynamicSection WhenLast(Func<FluentLayout[]> constraints)
        {
            whenLast = constraints;
            return this;
        }

        public DynamicSection Targets(params UIView[] views)
        {
            targets = views;
            return this;
        }

        public DynamicSection OnShown(Action callback)
        {
            onShown = callback;
            return this;
        }

        //todo[sk]: not implemented yet, may be not necessary to implement
        public DynamicSection OnBeforeShow(Action callback)
        {
            onBeforeShow = callback;
            return this;
        }
    }
}
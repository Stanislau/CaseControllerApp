using System.Collections.Generic;
using System.Linq;
using UIKit;
using Cirrious.FluentLayouts.Touch;

namespace Danfoss.CaseControllerApp.Apple.Extensions
{
    public static class FluentLayoutSyntaxExtension
    {
        public static DynamicLayout AddConstraints(this UIView view, params DynamicSection[] sections)
        {
            var dynamicLayout = new DynamicLayout(view, sections);
            return dynamicLayout;
        }

        public static void FillParentSize(this UIView view)
        {
            var parent = view.Superview;
            parent.AddConstraints(
                view.Top().EqualTo().TopOf(parent),
                view.Left().EqualTo().LeftOf(parent),
                view.Right().EqualTo().RightOf(parent),
                view.Bottom().EqualTo().BottomOf(parent)
                );
        }

        public static void PlaceToCenterOfParent(this UIView view)
        {
            var parent = view.Superview;
            parent.AddConstraints(
                view.CenterX().EqualTo().CenterXOf(parent),
                view.CenterY().EqualTo().CenterYOf(parent),
                view.Width().EqualTo().WidthOf(view)
                );
        }

        public static void SetHeightConstraintsOfSelf(this UIView view)
        {
            view.AddConstraints(view.HeightOfSelf());
        }

        public static void SetHeightConstraintEqualTo(this UIView view, float constant)
        {
            view.AddConstraints(view.Height().EqualTo(constant));
        }

        public static FluentLayout WidthOfSelf(this UIView view)
        {
            return view.Width().EqualTo().WidthOf(view);
        }

        public static FluentLayout WidthOfParent(this UIView view, float horizontal = 0)
        {
            return view.Width().EqualTo().WidthOf(view.Superview).Minus(horizontal * 2);
        }

        public static FluentLayout HeightOfSelf(this UIView view)
        {
            return view.Height().EqualTo().HeightOf(view);
        }

        public static FluentLayout HeightOfParent(this UIView view, float vertical = 0)
        {
            return view.Height().EqualTo().HeightOf(view.Superview).Minus(vertical * 2);
        }

        public static FluentLayout AtTopOfParent(this UIView view, float margin = 0)
        {
            return view.AtTopOf(view.Superview, margin);
        }

        public static FluentLayout AtBottomOfParent(this UIView view, float margin = 0)
        {
            return view.AtBottomOf(view.Superview, margin);
        }

        public static FluentLayout AtLeftOfParent(this UIView view, float margin = 0)
        {
            return view.AtLeftOf(view.Superview, margin);
        }

        public static FluentLayout AtRightOfParent(this UIView view, float margin = 0)
        {
            return view.AtRightOf(view.Superview, margin);
        }

        public static FluentLayout CenterXOfParent(this UIView view)
        {
            return view.CenterX().EqualTo().CenterXOf(view.Superview);
        }

        public static FluentLayout CenterYOfParent(this UIView view)
        {
            return view.CenterY().EqualTo().CenterYOf(view.Superview);
        }

        public static NSLayoutConstraint[] ToNative(this IEnumerable<FluentLayout> self)
        {
            return self
                .Where(fluent => fluent != null)
                .SelectMany(fluent => fluent.ToLayoutConstraints())
                .ToArray();
        }

        public static void AddFluentConstraints(this UIView view, params FluentLayout[] layouts)
        {
            view.AddConstraints(layouts);
        }
    }
}
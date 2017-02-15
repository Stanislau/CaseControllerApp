using System;
using System.Collections.Generic;
using System.Drawing;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Danfoss.CaseControllerApp.Apple.Extensions;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Controls
{
    public class DanfossPagerView : UIView
    {
        private Dictionary<int, UILabel> _numbers = new Dictionary<int, UILabel>();

        public DanfossPagerView(int pagesCount, int selectedItem)
        {
            VerticalLineView _line = null;

            for (int i = 0; i < pagesCount; i++)
            {
                var label = new UILabel();
                label.TextColor = selectedItem == (i + 1) ? UIColor.Red : UIColor.Black;
                label.Text = (i+1).ToString();
                _numbers[i] = label;
                label.TranslatesAutoresizingMaskIntoConstraints = false;
                this.Add(label);

                if (_line == null)
                {
                    this.AddFluentConstraints(
                        label.AtTopOfParent(10),
                        label.CenterXOfParent(),
                        label.WidthOfSelf(),
                        label.HeightOfSelf()
                        );
                }
                else
                {
                    this.AddFluentConstraints(
                        label.Top().EqualTo().BottomOf(_line).Plus(3),
                        label.CenterXOfParent(),
                        label.WidthOfSelf(),
                        label.HeightOfSelf()
                        );
                }


                if (i != pagesCount - 1) //not last
                {
                    _line = new VerticalLineView();
                    _line.TranslatesAutoresizingMaskIntoConstraints = false;
                    this.Add(_line);

                    this.AddFluentConstraints(
                        _line.Top().EqualTo().BottomOf(label).Plus(3), 
                        _line.CenterXOfParent(),
                        _line.Width().EqualTo(1),
                        _line.Height().EqualTo(50)
                        );

                }
            }
        }
    }

    public class VerticalLineView : UIView
    {
        private UIColor _color;
        public UIColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                SetNeedsDisplay();
            }
        }

        public VerticalLineView()
        {
            Color = UIColor.Red;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (var context = UIGraphics.GetCurrentContext())
            {
                var x = (rect.X + rect.Width) / 2;
                var y1 = rect.Y;
                var y2 = rect.Y + rect.Height;

                context.SetFillColor(Color.CGColor);
                context.SetStrokeColor(Color.CGColor);
                context.SetLineWidth(1);
                context.SaveState();
                context.MoveTo(x, y1);
                context.AddLineToPoint(x, y2);
                context.RestoreState();
            }
        }
    }
}
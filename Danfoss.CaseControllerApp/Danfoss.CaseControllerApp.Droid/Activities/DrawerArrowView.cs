using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Register("danfoss.DrawerArrowView")]
    public class DrawerArrowView : ImageView
    {
        private DrawerArrowDrawable _drawable;

        public DrawerArrowView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public DrawerArrowView(Context context) : base(context)
        {
            Initialize();
        }

        public DrawerArrowView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public DrawerArrowView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public DrawerArrowView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        private void Initialize()
        {
            _drawable = new DrawerArrowDrawable(Context, isMirrored: true);
            SetImageDrawable(_drawable);
            _drawable.Invalidate = () => InvalidateDrawable(_drawable);
        }

        public void Sync(DrawerLayout drawer)
        {
            Touch += (sender, args) =>
            {
                if (drawer.IsDrawerOpen((int)GravityFlags.End))
                {
                    drawer.CloseDrawers();
                }
                else
                {
                    drawer.OpenDrawer((int)GravityFlags.End);
                }
            };
            drawer.DrawerSlide += (sender, args) => _drawable.SetProgress(args.SlideOffset);
        }
    }
}
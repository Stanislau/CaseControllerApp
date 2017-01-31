using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Register("danfoss.DrawerLayout")]
    public class DrawerLayoutEx : DrawerLayout
    {
        public DrawerLayoutEx(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public DrawerLayoutEx(Context context) : base(context)
        {
        }

        public DrawerLayoutEx(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public DrawerLayoutEx(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public int DrawerLockMode
        {
            get { return this.GetDrawerLockMode((int) GravityFlags.End); }
            set
            {
                SetDrawerLockMode(value);
            }
        }
    }
}
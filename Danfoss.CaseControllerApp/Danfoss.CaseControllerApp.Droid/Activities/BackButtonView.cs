using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    [Register("danfoss.BackButtonView")]
    public class BackButtonView : ImageView
    {
        private BackArrowDrawable _drawable;

        public BackButtonView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public BackButtonView(Context context) : base(context)
        {
            Initialize();
        }

        public BackButtonView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public BackButtonView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public BackButtonView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        private void Initialize()
        {
            _drawable = new BackArrowDrawable(Context, isMirrored: false);
            SetImageDrawable(_drawable);
        }
    }
}
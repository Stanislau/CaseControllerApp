using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Daven.SyntaxExtensions;
using Math = Java.Lang.Math;

namespace Danfoss.CaseControllerApp.Droid.Activities
{
    public class BackArrowDrawable : Drawable
    {
        private static float ARROW_HEAD_ANGLE = (float)Math.ToRadians(45.0d);
        protected float mBarGap;
        protected float mBarSize;
        protected float mBarThickness;
        protected float mMiddleArrowSize;
        protected Paint mPaint = new Paint();
        protected Path mPath = new Path();
        protected float mProgress;
        protected int mSize;
        protected float mVerticalMirror = 1f;
        protected float mTopBottomArrowSize;
        private bool _isMirrored;

        public BackArrowDrawable(Context context, bool isMirrored = false)
        {
            _isMirrored = isMirrored;
            this.mPaint.Color = context.Resources.GetColor(Resource.Color.ldrawer_color);
            this.mSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_drawableSize);
            this.mBarSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_barSize);
            this.mTopBottomArrowSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_topBottomBarArrowSize);
            this.mBarThickness = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_thickness);
            this.mBarGap = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_gapBetweenBars);
            this.mMiddleArrowSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_middleBarArrowSize);
            this.mPaint.SetStyle(Paint.Style.Stroke);
            this.mPaint.StrokeJoin = Paint.Join.Round;
            this.mPaint.StrokeCap = Paint.Cap.Square;
            this.mPaint.StrokeWidth = this.mBarThickness;
        }

        protected float GetValue(float start, float end, float progress)
        {
            return start + progress * (end - start);
        }

        public override void Draw(Canvas canvas)
        {
            var localRect = Bounds;
            var arrowSize = mTopBottomArrowSize;
            var middleSize = mMiddleArrowSize;
            var centerHorizontally = mBarThickness/2.0F;
            var angle = ARROW_HEAD_ANGLE;
            var pi = 180.0F;
            var centerVertically = -middleSize / 2.0F;
            var dxSize = (float)Math.Round(arrowSize * Math.Cos(angle));
            var dySize = (float)Math.Round(arrowSize * Math.Sin(angle));

            mPath.Rewind();
            //mPath.MoveTo(centerVertically + centerHorizontally, 0.0F);
            //mPath.RLineTo(middleSize - centerHorizontally, 0.0F);
            mPath.MoveTo(centerVertically, 0.0F);
            mPath.RLineTo(dxSize, dySize);
            mPath.MoveTo(centerVertically, 0.0F);
            mPath.RLineTo(dxSize, -dySize);
            mPath.MoveTo(0.0F, 0.0F);
            mPath.Close();
            canvas.Save();
            
            if (_isMirrored == false)
                canvas.Rotate(180.0F, localRect.CenterX(), localRect.CenterY());
            canvas.Rotate(pi * mVerticalMirror, localRect.CenterX(), localRect.CenterY());
            canvas.Translate(localRect.CenterX(), localRect.CenterY());
            canvas.DrawPath(this.mPath, this.mPaint);
            canvas.Restore();
        }

        public override void SetAlpha(int alpha)
        {
            this.mPaint.Alpha = Alpha;
        }

        public override void SetColorFilter(ColorFilter cf)
        {
            this.mPaint.SetColorFilter(cf);
        }

        public override int Opacity => (int)Format.Translucent;
    }

    public class DrawerArrowDrawable : Drawable
    {
        private static float ARROW_HEAD_ANGLE = (float)Math.ToRadians(45.0d);
        protected float mBarGap;
        protected float mBarSize;
        protected float mBarThickness;
        protected float mMiddleArrowSize;
        protected Paint mPaint = new Paint();
        protected Path mPath = new Path();
        protected float mProgress;
        protected int mSize;
        protected float mVerticalMirror = 1f;
        protected float mTopBottomArrowSize;
        protected Context context;

        private bool _isMirrored = false;

        public DrawerArrowDrawable(Context context, bool isMirrored = false)
        {
            _isMirrored = isMirrored;
            this.context = context;
            mPaint.AntiAlias = true;
            //todo[sk]: find a way to get color from resources
            this.mPaint.Color = context.Resources.GetColor(Resource.Color.ldrawer_color);
            this.mSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_drawableSize);
            this.mBarSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_barSize);
            this.mTopBottomArrowSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_topBottomBarArrowSize);
            this.mBarThickness = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_thickness);
            this.mBarGap = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_gapBetweenBars);
            this.mMiddleArrowSize = context.Resources.GetDimensionPixelSize(Resource.Dimension.ldrawer_middleBarArrowSize);
            this.mPaint.SetStyle(Paint.Style.Stroke);
            this.mPaint.StrokeJoin = Paint.Join.Round;
            this.mPaint.StrokeCap = Paint.Cap.Square;
            this.mPaint.StrokeWidth = this.mBarThickness;
        }

        protected float lerp(float paramFloat1, float paramFloat2, float paramFloat3)
        {
            return paramFloat1 + paramFloat3 * (paramFloat2 - paramFloat1);
        }

        public override void Draw(Canvas canvas)
        {
            var localRect = Bounds;
            var f1 = lerp(this.mBarSize, this.mTopBottomArrowSize, this.mProgress);
            var f2 = lerp(this.mBarSize, this.mMiddleArrowSize, this.mProgress);
            var f3 = lerp(0.0F, this.mBarThickness / 2.0F, this.mProgress);
            var f4 = lerp(0.0F, ARROW_HEAD_ANGLE, this.mProgress);
            var f5 = 0.0F;
            var f6 = 180.0F;
            var f7 = lerp(f5, f6, this.mProgress);
            var f8 = lerp(this.mBarGap + this.mBarThickness, 0.0F, this.mProgress);
            mPath.Rewind();
            var f9 = -f2 / 2.0F;
            mPath.MoveTo(f9 + f3, 0.0F);
            mPath.RLineTo(f2 - f3, 0.0F);
            var f10 = (float)Math.Round(f1 * Math.Cos(f4));
            var f11 = (float)Math.Round(f1 * Math.Sin(f4));
            mPath.MoveTo(f9, f8);
            mPath.RLineTo(f10, f11);
            mPath.MoveTo(f9, -f8);
            mPath.RLineTo(f10, -f11);
            mPath.MoveTo(0.0F, 0.0F);
            mPath.Close();
            canvas.Save();
            if (_isMirrored == false)
                canvas.Rotate(180.0F, localRect.CenterX(), localRect.CenterY());
            canvas.Rotate(f7 * mVerticalMirror, localRect.CenterX(), localRect.CenterY());
            canvas.Translate(localRect.CenterX(), localRect.CenterY());
            canvas.DrawPath(this.mPath, this.mPaint);
            canvas.Restore();
        }

        public int GetIntrinsicHeight()
        {
            return this.mSize;
        }

        public int GetIntrinsicWidth()
        {
            return this.mSize;
        }

        public override void SetAlpha(int alpha)
        {
            this.mPaint.Alpha = Alpha;
        }

        public override void SetColorFilter(ColorFilter cf)
        {
            this.mPaint.SetColorFilter(cf);
        }

        public override int Opacity => (int)Format.Translucent;

        public void SetVerticalMirror(bool isVerticalMirror)
        {
            this.mVerticalMirror = isVerticalMirror ? 1 : -1;
        }

        public void SetProgress(float paramFloat)
        {
            this.mProgress = paramFloat;
            Invalidate.Call();
        }

        public Action Invalidate { get; set; }

        public void SetColor(int resourceId)
        {
            //todo[sk]: find a way to get color from resources
            mPaint.Color = context.Resources.GetColor(resourceId);
        }
    }
}
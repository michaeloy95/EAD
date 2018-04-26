using Android.Content;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Widget;
using FitnessApp.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FitnessApp.Controls.WhiteEntry), typeof(WhiteEntryRenderer))]
namespace FitnessApp.Droid.Renderers
{
    public class WhiteEntryRenderer : EntryRenderer
    {
        public WhiteEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;
            this.Control.SetBackground(ContextCompat.GetDrawable(Context, Resource.Drawable.WhiteEntryBorder));
            this.Control.SetHighlightColor(Android.Graphics.Color.ParseColor("#FAFAFA"));

            IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
            IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
            JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, 0);
        }
    }
}
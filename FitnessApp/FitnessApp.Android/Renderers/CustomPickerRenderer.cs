using Android.Content;
using Android.Views;
using FitnessApp.Controls;
using FitnessApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace FitnessApp.Droid.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && e.NewElement != null)
            {
                var element = e.NewElement as CustomPicker;
                this.Control.Gravity = (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Start) ? GravityFlags.Start
                    : (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center) ? GravityFlags.CenterHorizontal
                    : (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.End) ? GravityFlags.End
                    : GravityFlags.Start;
            }
        }
    }
}
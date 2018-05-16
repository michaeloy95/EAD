using Xamarin.Forms;

namespace FitnessApp.Controls
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(
                nameof(HorizontalTextAlignment),
                typeof(TextAlignment),
                typeof(CustomPicker),
                default(TextAlignment));

        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(
                nameof(VerticalTextAlignment),
                typeof(TextAlignment),
                typeof(CustomPicker),
                default(TextAlignment));

        public TextAlignment VerticalTextAlignment
        {
            get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public CustomPicker()
        {
        }
    }
}

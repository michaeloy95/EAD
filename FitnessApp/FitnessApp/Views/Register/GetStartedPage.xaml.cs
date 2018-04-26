using FitnessApp.ViewModels.Register;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetStartedPage : ContentPage
    {
        public GetStartedViewModel ViewModel;

        public GetStartedPage()
        {
            InitializeComponent();

            this.ViewModel = new GetStartedViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }
    }
}
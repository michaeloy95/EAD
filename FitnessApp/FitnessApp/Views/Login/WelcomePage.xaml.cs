using FitnessApp.ViewModels.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomeViewModel ViewModel;

        public WelcomePage()
        {
            InitializeComponent();

            this.ViewModel = new WelcomeViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }
    }
}
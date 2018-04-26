using FitnessApp.Views.Login;
using FitnessApp.Views.Register;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Login
{
    public class WelcomeViewModel : BaseViewModel
    {
        public ICommand GetStartedCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public WelcomeViewModel()
        {
            GetStartedCommand = new Command(async() => await this.NavigationService.CurrentPage.Navigation.PushAsync(new RegisterEmailPage()));
            LoginCommand = new Command(async() => await this.NavigationService.CurrentPage.Navigation.PushAsync(new LoginPage()));
        }
    }
}

using FitnessApp.Common;
using FitnessApp.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        private string username = string.Empty;
        public string Username
        {
            get { return username; }
            set { SetProperty<string>(ref username, value); }
        }

        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { SetProperty<string>(ref password, value); }
        }

        public LoginViewModel()
        {
            this.Title = "Login";

            this.LoginCommand = new Command(ExecuteLogin);
        }

        private void ExecuteLogin()
        {
            if (Username == "admin" && Password == "admin")
            {
                // Store to local settings
                this.User.SetLogin(true);

                this.NotificationManager.ShowNotification(Constants.NotificationOthers, "Logged In", "You have successfully logged in");

                if (Device.RuntimePlatform == Device.iOS)
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
            }
            else
            {
                this.NavigationService.CurrentPage.DisplayAlert("Login Failed", "Wrong Username or Password", "OK");
            }
        }
    }
}

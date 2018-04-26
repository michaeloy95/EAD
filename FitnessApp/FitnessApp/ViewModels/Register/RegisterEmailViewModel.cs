using FitnessApp.Views.Register;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Register
{
    public class RegisterEmailViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; private set; }

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { SetProperty<string>(ref email, value); }
        }

        public RegisterEmailViewModel()
        {
            NextCommand = new Command(GotoNextPage);
        }

        private async void GotoNextPage()
        {
            if (!Email.Contains("@") || !Email.Contains(".") || Email.Length < 5)
            {
                await this.NavigationService.CurrentPage.DisplayAlert("Invalid Email", "You seem to enter your email in a wrong format. Please check again", "OK");
                return;
            }

            Application.Current.Properties["username"] = Email;
            await this.NavigationService.CurrentPage.Navigation.PushAsync(new RegisterNamePage());
        }
    }
}

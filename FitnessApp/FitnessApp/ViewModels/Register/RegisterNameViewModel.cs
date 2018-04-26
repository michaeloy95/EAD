using FitnessApp.Views.Register;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Register
{
    public class RegisterNameViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; private set; }

        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        public RegisterNameViewModel()
        {
            NextCommand = new Command(GotoNextPage);
        }

        private async void GotoNextPage()
        {
            if (Name.Length < 2)
            {
                await this.NavigationService.CurrentPage.DisplayAlert("Invalid Name", "Please enter your name", "OK");
                return;
            }

            Application.Current.Properties["name"] = Name;
            await this.NavigationService.CurrentPage.Navigation.PushAsync(new RegisterLocationPage());
        }
    }
}

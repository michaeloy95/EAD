using FitnessApp.Views.Register;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Register
{
    public class RegisterLocationViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; private set; }

        public string LocationText
        {
            get
            {
                return $"Hi, {Application.Current.Properties["name"]}.\n" +
                  $"We can help to connect you with people nearby. " +
                  $"Please tell us where you are.";
            }
        }

        private string location = string.Empty;
        public string Location
        {
            get { return location; }
            set { SetProperty<string>(ref location, value); }
        }

        public RegisterLocationViewModel()
        {
            NextCommand = new Command(GotoNextPage);
        }

        private async void GotoNextPage()
        {
            if (String.IsNullOrEmpty(Location))
            {
                await this.NavigationService.CurrentPage.DisplayAlert("Invalid Location", "Please specify your location", "OK");
                return;
            }

            Application.Current.Properties["location"] = Location;
            await this.NavigationService.CurrentPage.Navigation.PushAsync(new GetStartedPage());
        }
    }
}

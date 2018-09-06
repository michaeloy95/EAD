using FitnessApp.Interfaces;
using FitnessApp.Models;
using FitnessApp.Views.Login;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private IList<SettingsMenu> settingsMenuList;
        public IList<SettingsMenu> SettingsMenuList
        {
            get { return this.settingsMenuList; }
            set { SetProperty<IList<SettingsMenu>>(ref this.settingsMenuList, value); }
        }

        public ICommand SelectMenuCommand;

        public SettingsViewModel()
        {
            this.SelectMenuCommand = new Command<SettingsMenu>(this.ExecuteMenu);
            this.PrepareMenuItems();
        }

        private void PrepareMenuItems()
        {
            this.SettingsMenuList = new List<SettingsMenu>()
            {
                new SettingsMenu()
                {
                    Title = "Logout",
                    Action = Logout
                },
            };
        }

        private void ExecuteMenu(SettingsMenu menu)
        {
            try
            {
                menu.Action();
            }
            catch
            {
                DependencyService.Get<IMessageHelper>().ShortAlert("This option is not available.");
            }
        }

        private async void Logout()
        {
            if (this.IsBusy)
                return;
            this.IsBusy = true;

            bool logout = await this.NavigationService.CurrentPage.DisplayAlert(
                "Logout",
                "Do you wish to logout from your account?",
                "Logout",
                "Cancel");

            if (logout)
            {
                this.User.SetLogin(false);

                App.Current.MainPage = new NavigationPage(new WelcomePage());
            }

            this.IsBusy = false;
        }
    }
}

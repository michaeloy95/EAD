using FitnessApp.Models;
using FitnessApp.ViewModels.Settings;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsViewModel ViewModel;

        public SettingsPage()
        {
            InitializeComponent();

            this.ViewModel = new SettingsViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }

        private void SettingsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SettingsMenu;
            if (item == null)
                return;

            if (this.ViewModel.SelectMenuCommand.CanExecute(item))
            {
                this.ViewModel.SelectMenuCommand.Execute(item);
            }

            this.SettingsListView.SelectedItem = null;
        }
    }
}
using FitnessApp.ViewModels.Profile;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public EditProfileViewModel ViewModel;

        public EditProfilePage()
        {
            InitializeComponent();

            this.ViewModel = new EditProfileViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ViewModel.IsUpdated = true;
        }

        private void BirthdayText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ViewModel.IsUpdated = true;

            if (e.NewTextValue.Length == 2 && e.OldTextValue.Length < 2)
            {
                this.BirthdayEntry.Text += "/";
            }
            else if (e.NewTextValue.Length == 5 && e.OldTextValue.Length < 5)
            {
                this.BirthdayEntry.Text += "/";
            }
            else if (e.NewTextValue.Length == 3 && e.OldTextValue.Length > 3)
            {
                this.BirthdayEntry.Text = this.BirthdayEntry.Text.Substring(0, this.BirthdayEntry.Text.Length - 1);
            }
            else if (e.NewTextValue.Length == 6 && e.OldTextValue.Length > 6)
            {
                this.BirthdayEntry.Text = this.BirthdayEntry.Text.Substring(0, this.BirthdayEntry.Text.Length - 1);
            }
            else if (e.NewTextValue.Length == 10)
            {
                this.ViewModel.Birthday = Convert.ToDateTime(e.NewTextValue);
            }
        }
    }
}
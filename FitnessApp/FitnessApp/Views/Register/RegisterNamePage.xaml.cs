using FitnessApp.ViewModels.Register;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterNamePage : ContentPage
    {
        public RegisterNameViewModel ViewModel;

        public RegisterNamePage()
        {
            InitializeComponent();

            this.ViewModel = new RegisterNameViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;

            this.Appearing += (object sender, EventArgs e) => NameEntry.Focus();
        }
    }
}
using FitnessApp.ViewModels.Register;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterLocationPage : ContentPage
    {
        public RegisterLocationViewModel ViewModel;

        public RegisterLocationPage()
        {
            InitializeComponent();

            this.ViewModel = new RegisterLocationViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;

            this.Appearing += (object sender, EventArgs e) => LocationEntry.Focus();
        }
    }
}
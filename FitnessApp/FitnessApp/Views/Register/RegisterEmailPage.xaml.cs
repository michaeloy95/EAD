using FitnessApp.ViewModels.Register;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterEmailPage : ContentPage
    {
        public RegisterEmailViewModel ViewModel;

        public RegisterEmailPage()
        {
            InitializeComponent();

            this.ViewModel = new RegisterEmailViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;

            this.Appearing += (object sender, EventArgs e) => EmailEntry.Focus();
        }
    }
}
using FitnessApp.Data;
using FitnessApp.Interfaces;
using FitnessApp.Services;
using FitnessApp.Views;
using FitnessApp.Views.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FitnessApp
{
    public partial class App : Application
    {
        public static User user = null;
        public static User User
        {
            get
            {
                user = user ?? new User();
                return user;
            }
        }

        public static INavigationService navigationService = null;
        public static INavigationService NavigationService
        {
            get
            {
                navigationService = navigationService ?? new NavigationService();
                return navigationService;
            }
        }

        public App()
        {
            this.InitializeComponent();

            if (!User.HasLoggedIn)
            {
                if (Device.RuntimePlatform == Device.iOS)
                    this.MainPage = new MainPage();
                else
                    this.MainPage = new NavigationPage(new MainPage())
                    {
                        Icon = "icon_sil.png"
                    };
            }
            else
            {
                this.MainPage = new NavigationPage(new WelcomePage());
            }
        }
    }
}
using FitnessApp.Data;
using FitnessApp.Interfaces;
using FitnessApp.Models;
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

        public static ILocalDatabase<Meal> localDatabaseMeal = null;
        public static ILocalDatabase<Meal> LocalDatabaseMeal
        {
            get
            {
                if (localDatabaseMeal == null)
                {
                    localDatabaseMeal = new LocalDatabaseMeal(DependencyService.Get<IFileHelper>().GetLocalFilePath("LocalDBMeal.db3"));
                }
                return localDatabaseMeal;
            }
        }

        public static ILocalDatabase<Food> localDatabaseFood = null;
        public static ILocalDatabase<Food> LocalDatabaseFood
        {
            get
            {
                if (localDatabaseFood == null)
                {
                    localDatabaseFood = new LocalDatabaseFood(DependencyService.Get<IFileHelper>().GetLocalFilePath("LocalDBFood.db3"));
                }
                return localDatabaseFood;
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
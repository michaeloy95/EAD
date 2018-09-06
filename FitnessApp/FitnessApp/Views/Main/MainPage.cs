using FitnessApp.Views.Board;
using FitnessApp.Views.Food;
using FitnessApp.Views.Profile;
using FitnessApp.Views.Settings;
using Xamarin.Forms;

namespace FitnessApp.Views
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, profilePage, foodPage, settingsPage;
            
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new BoardPage())
                    {
                        Title = "Home",
                        Icon = "tab_feed.png"
                    };

                    profilePage = new NavigationPage(new ProfilePage())
                    {
                        Title = "Profile",
                        Icon = "tab_about.png"
                    };

                    foodPage = new NavigationPage(new MyFoodListPage())
                    {
                        Title = "Food"
                    };

                    settingsPage = new NavigationPage(new SettingsPage())
                    {
                        Title = "Settings"
                    };
                    break;
                default:
                    itemsPage = new BoardPage()
                    {
                        Title = "Home"
                    };

                    profilePage = new ProfilePage()
                    {
                        Title = "Profile"
                    };

                    foodPage = new MyFoodListPage()
                    {
                        Title = "Food"
                    };

                    settingsPage = new SettingsPage()
                    {
                        Title = "Settings"
                    };
                    break;
            }

            Children.Add(itemsPage);
            Children.Add(profilePage);
            Children.Add(foodPage);
            Children.Add(settingsPage);

            this.BarTextColor = Color.White;
            this.Icon = "icon_sil.png";
            this.Title = "Exercise Diet App";
        }
    }
}

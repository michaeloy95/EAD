using FitnessApp.ViewModels.Profile;
using Xamarin.Forms;

#if __ANDROID__
using Xamarin.Forms.Platform.Android;
using FitnessApp.Droid; //Your Namespace
using Android.Views;
#endif

namespace FitnessApp.Views.Profile
{
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel ViewModel;

        public ProfilePage()
        {
            InitializeComponent();

            this.ViewModel = new ProfileViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;



#if __ANDROID__
            var fab = new CheckableFab(Forms.Context)
            {
                UseCompatPadding = true,
                WidthRequest = 100
            };
           
            fab.SetImageResource(Droid.Resource.Drawable.icon_sil);
            fab.Click += async (sender, e) =>
            {
                await MainPage.DisplayAlert("Native FAB Clicked", 
                                                        "Whoa!!!!!!", "OK");
            };

            StackLayout stack = new StackLayout();
            stack.Children.Add(fab);
            this.GridLayout.Children.Add(stack, 0, 1, 0, 1);
#endif
        }

        private void MealListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            if (this.ViewModel.SelectMealCommand.CanExecute(e.SelectedItem))
            {
                this.ViewModel.SelectMealCommand.Execute(e.SelectedItem);
            }

            this.MealListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.ViewModel.RefreshItem();
        }
    }
}

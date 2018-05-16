using FitnessApp.ViewModels.Meal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Meal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMealPage : ContentPage
    {
        public AddMealViewModel ViewModel;

        public AddMealPage(Models.Food food)
        {
            InitializeComponent();

            this.ViewModel = new AddMealViewModel(food)
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }

        private void FoodListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.FoodListView.SelectedItem = null;
        }
    }
}
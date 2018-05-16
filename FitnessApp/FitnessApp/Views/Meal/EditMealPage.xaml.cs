using FitnessApp.ViewModels.Meal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Meal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMealPage : ContentPage
    {
        public EditMealViewModel ViewModel;

        public EditMealPage(Models.Meal meal)
        {
            InitializeComponent();

            this.ViewModel = new EditMealViewModel(meal)
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }

        private void FoodListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            if (this.ViewModel.SelectFoodCommand.CanExecute(null))
            {
                this.ViewModel.SelectFoodCommand.Execute(null);
            }

            this.FoodListView.SelectedItem = null;
        }
    }
}
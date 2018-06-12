using FitnessApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.ViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealViewCell : ViewCell
    {
        public MealViewCell()
        {
            InitializeComponent();
        }

        protected override async void OnBindingContextChanged()
        {
            var item = BindingContext as Models.Meal;
            if (item == null)
            {
                return;
            }
            
            var SelectedFood = await App.LocalDatabaseFood.GetItemAsync(item.FoodID);
           
            this.FoodNameLabel.Text = SelectedFood.Name;
            this.FoodCaloriesLabel.Text = SelectedFood.Calories.ToString();
            this.FoodCarbsLabel.Text = SelectedFood.Carbs.ToString();
            this.FoodProteinLabel.Text = SelectedFood.Protein.ToString();
            this.FoodFatLabel.Text = SelectedFood.Fat.ToString();

            base.OnBindingContextChanged();
        }
    }
}
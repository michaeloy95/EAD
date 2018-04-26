using FitnessApp.Views.Food;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class AddFoodViewModel : BaseViewModel
    {
        public ICommand SearchFoodCommand { get; private set; }

        public ICommand AddFoodCommand { get; private set; }

        public AddFoodViewModel()
        {
            this.SearchFoodCommand = new Command(this.SearchFood);
            this.AddFoodCommand = new Command(this.AddFood);
        }

        private void SearchFood()
        {
            this.NavigationService.NavigateTo(typeof(SearchFoodPage));
        }

        private void AddFood()
        {
            this.NavigationService.SwitchTo(typeof(FoodIsAddedPage));
        }
    }
}

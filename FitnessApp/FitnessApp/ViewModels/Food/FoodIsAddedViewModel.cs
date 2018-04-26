using FitnessApp.Views.Food;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class FoodIsAddedViewModel : BaseViewModel
    {
        public ICommand AddMoreCommand { get; private set; }

        public ICommand DoneCommand { get; private set; }

        public FoodIsAddedViewModel()
        {
            this.AddMoreCommand = new Command(this.AddMore);
            this.DoneCommand = new Command(this.Done);
        }

        private void AddMore()
        {
            this.NavigationService.SwitchTo(typeof(AddFoodPage));
        }

        private void Done()
        {
            this.NavigationService.GoBack();
        }
    }
}

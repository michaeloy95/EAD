using FitnessApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class SearchFoodViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Food> foodList;
        public ObservableCollection<Models.Food> FoodList
        {
            get { return this.foodList; }
            set { this.SetProperty<ObservableCollection<Models.Food>>(ref this.foodList, value); }
        }

        private string searchEntryText = string.Empty;
        public string SearchEntryText
        {
            get { return this.searchEntryText; }
            set { SetProperty<string>(ref this.searchEntryText, value); }
        }

        public ICommand SearchCommand { get; private set; }

        public SearchFoodViewModel()
        {
            this.SearchCommand = new Command(this.SearchFood);
        }

        private async void SearchFood()
        {
            await FatSecretPlatformAPIService.GetFoods(this.SearchEntryText);
        }
    }
}

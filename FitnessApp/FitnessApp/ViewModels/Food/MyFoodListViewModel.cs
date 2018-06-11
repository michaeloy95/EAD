using FitnessApp.Views.Food;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class MyFoodListViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Food> foodList = new ObservableCollection<Models.Food>();
        public ObservableCollection<Models.Food> FoodList
        {
            get { return this.foodList; }
            set { this.SetProperty<ObservableCollection<Models.Food>>(ref this.foodList, value); }
        }

        private ObservableCollection<Models.Food> filteredFoodList = new ObservableCollection<Models.Food>();
        public ObservableCollection<Models.Food> FilteredFoodList
        {
            get { return this.filteredFoodList; }
            set { this.SetProperty<ObservableCollection<Models.Food>>(ref this.filteredFoodList, value); }
        }
        private bool emptyFoodLayoutVisible = true;
        public bool EmptyFoodLayoutVisible
        {
            get { return this.emptyFoodLayoutVisible; }
            set { this.SetProperty<bool>(ref this.emptyFoodLayoutVisible, value); }
        }

        private bool foodLayoutVisible = false;
        public bool FoodLayoutVisible
        {
            get { return this.foodLayoutVisible; }
            set
            {
                this.SetProperty<bool>(ref this.foodLayoutVisible, value);
                this.EmptyFoodLayoutVisible = !this.foodLayoutVisible;
            }
        }

        private string searchEntryText;
        public string SearchEntryText
        {
            get { return this.searchEntryText; }
            set
            {
                this.searchEntryText = value;
                if (string.IsNullOrWhiteSpace(this.searchEntryText))
                    this.FilteredFoodList = new ObservableCollection<Models.Food>(FoodList);
                else
                    this.FilteredFoodList = new ObservableCollection<Models.Food>(FoodList
                        .Where(x => x.Name.Contains(this.searchEntryText)));
            }
        }

        public ICommand AddCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public MyFoodListViewModel()
        {
            this.AddCommand = new Command(this.AddFood);
            this.EditCommand = new Command<Models.Food>(this.EditFood);
        }

        public async void RefreshItem()
        {
            if (!this.User.FoodIsLoaded)
            {
                this.FoodList = new ObservableCollection<Models.Food>((await this.LocalDatabaseFood.GetItemsAsync()).OrderBy(f => f.Name).ToList());
                this.FilteredFoodList = new ObservableCollection<Models.Food>(FoodList);
                this.FoodLayoutVisible = this.FoodList.Count > 0;
            }

            this.User.FoodIsLoaded = true;
        }

        private void AddFood()
        {
            this.NavigationService.NavigateTo(typeof(AddFoodPage));
        }

        private void EditFood(Models.Food food)
        {
            this.NavigationService.NavigateTo(typeof(EditFoodPage), new object[1] { food });
        }
    }
}

using FitnessApp.Views.Food;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Meal
{
    public class SelectEditFoodViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Food> foodList;
        public ObservableCollection<Models.Food> FoodList
        {
            get { return this.foodList; }
            set { this.SetProperty<ObservableCollection<Models.Food>>(ref this.foodList, value); }
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

        public ICommand AddFoodCommand { get; private set; }

        public ICommand SelectFoodCommand { get; private set; }

        public SelectEditFoodViewModel()
        {
            this.AddFoodCommand = new Command(this.AddFood);
            this.SelectFoodCommand = new Command<Models.Food>(this.SelectFood);
        }

        public async void RefreshItems()
        {
            this.FoodList = new ObservableCollection<Models.Food>((await this.LocalDatabaseFood.GetItemsAsync()).OrderBy(f => f.Name).ToList());
            this.FoodLayoutVisible = this.FoodList.Count > 0;
        }

        private void AddFood()
        {
            this.NavigationService.NavigateTo(typeof(AddFoodPage));
        }

        private void SelectFood(Models.Food food)
        {
            MessagingCenter.Send<SelectEditFoodViewModel, Models.Food>(this, "Select Food", food);
            this.NavigationService.GoBack();
        }
    }
}

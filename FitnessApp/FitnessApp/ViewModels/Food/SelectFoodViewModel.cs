using FitnessApp.Views.Food;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class SelectFoodViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Food> foodList;
        public ObservableCollection<Models.Food> FoodList
        {
            get { return this.foodList; }
            set { this.SetProperty<ObservableCollection<Models.Food>>(ref this.foodList, value); }
        }

        public ICommand AddFoodCommand { get; private set; }

        public SelectFoodViewModel()
        {
            this.AddFoodCommand = new Command(this.AddFood);
            this.Initialise();
        }

        public void Initialise()
        {
            this.FoodList = new ObservableCollection<Models.Food>()
            {
                new Models.Food()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Toasty",
                    Calories = 110,
                    Protein = 5,
                    Hydrates = 10,
                    Fat = 2,
                    ImageSource = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS13ySkAuUsBe8HqxvUfc6aphndKvmO-rwYx4UTkj7laqzLdEH4"
                },
                new Models.Food()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Chicken Steak",
                    Calories = 220,
                    Protein = 15,
                    Hydrates = 40,
                    Fat = 11,
                    ImageSource = "https://image.freepik.com/free-photo/grill-chicken-steak_1339-1222.jpg"
                }
            };
        }

        private void AddFood()
        {
            this.NavigationService.NavigateTo(typeof(AddFoodPage));
        }
    }
}

using FitnessApp.Views.Food;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Meal> mealList;
        public ObservableCollection<Models.Meal> MealList
        {
            get { return this.mealList; }
            set { this.SetProperty<ObservableCollection<Models.Meal>>(ref this.mealList, value); }
        }

        private bool mealListIsEmpty;
        public bool MealListIsEmpty
        {
            get { return this.mealListIsEmpty; }
            set
            {
                this.SetProperty<bool>(ref this.mealListIsEmpty, value);
                this.MealListIsNotEmpty = !this.mealListIsEmpty;
            }
        }

        private bool mealListIsNotEmpty;
        public bool MealListIsNotEmpty
        {
            get { return this.mealListIsNotEmpty; }
            set { this.SetProperty<bool>(ref this.mealListIsNotEmpty, value); }
        }

        public ICommand AddMealCommand { get; private set; }

        public ProfileViewModel()
        {
            this.AddMealCommand = new Command(AddMeal);

            this.Initialise();
            this.MealListIsEmpty = this.MealList.Count == 0;
        }

        public void Initialise()
        {
            this.MealList = new ObservableCollection<Models.Meal>()
            {
                new Models.Meal()
                {
                    ID = Guid.NewGuid().ToString(),
                    MealType = Enums.MealType.Breakfast,
                    DateTime = new DateTime(2018, 04, 09, 09, 33, 15),
                    Food = new Models.Food()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Toasty",
                        Calories = 110,
                        Protein = 5,
                        Carbs = 10,
                        Fat = 2,
                        ImageSource = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS13ySkAuUsBe8HqxvUfc6aphndKvmO-rwYx4UTkj7laqzLdEH4"
                    }
                },
                new Models.Meal()
                {
                    ID = Guid.NewGuid().ToString(),
                    MealType = Enums.MealType.Dinner,
                    DateTime = new DateTime(2018, 04, 09, 18, 24, 11),
                    Food = new Models.Food()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Chicken Steak",
                        Calories = 220,
                        Protein = 15,
                        Carbs = 40,
                        Fat = 11,
                        ImageSource = "https://image.freepik.com/free-photo/grill-chicken-steak_1339-1222.jpg"
                    }
                }
            };
        }

        public void AddMeal()
        {
            this.NavigationService.NavigateTo(typeof(SelectFoodPage));
        }
    }
}
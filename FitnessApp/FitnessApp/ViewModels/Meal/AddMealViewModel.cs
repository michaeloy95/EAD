using FitnessApp.Enums;
using FitnessApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Meal
{
    public class AddMealViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Food> foodList = new ObservableCollection<Models.Food>();
        public ObservableCollection<Models.Food> FoodList
        {
            get { return this.foodList; }
            set { this.SetProperty<ObservableCollection<Models.Food>>(ref this.foodList, value); }
        }
        
        public IList<string> MealTypeItems
        {
            get { return new List<string>(Enum.GetNames(typeof(MealType))); }
        }

        private int mealTypeIndex;
        public int MealTypeIndex
        {
            get { return this.mealTypeIndex; }
            set { this.SetProperty<int>(ref this.mealTypeIndex, value); }
        }

        private TimeSpan mealTime = DateTime.Now.TimeOfDay;
        public TimeSpan MealTime
        {
            get { return this.mealTime; }
            set { this.SetProperty<TimeSpan>(ref this.mealTime, value); }
        }

        private DateTime mealDate = DateTime.Now;
        public DateTime MealDate
        {
            get { return this.mealDate; }
            set { this.SetProperty<DateTime>(ref this.mealDate, value); }
        }

        private string descriptionText;
        public string DescriptionText
        {
            get { return this.descriptionText; }
            set { this.SetProperty<string>(ref this.descriptionText, value); }
        }

        public ICommand AddMealCommand { get; private set; }

        public AddMealViewModel(Models.Food food)
        {
            this.AddMealCommand = new Command(this.AddMeal);
            this.FoodList.Add(food);
        }

        public async void AddMeal()
        {
            var message = DependencyService.Get<IMessageHelper>();

            if (this.IsBusy)
            {
                return;
            }
            this.IsBusy = true;

            Models.Meal meal = new Models.Meal()
            {
                ID = Guid.NewGuid().ToString(),
                FoodID = this.FoodList[0].ID ?? null,
                DateTime = new DateTime(
                    this.MealDate.Year,
                    this.MealDate.Month,
                    this.MealDate.Day,
                    this.MealTime.Hours,
                    this.MealTime.Minutes,
                    this.MealTime.Seconds),
                MealType = (MealType)this.MealTypeIndex,
                Description = this.DescriptionText ?? ""
            };

            await this.LocalDatabaseMeal.SaveItemAsync(meal);
            message.ShortAlert("New meal is successfully added.");
            this.User.MealIsLoaded = false;
            this.NavigationService.GoBack();
            this.NavigationService.GoBack();

            this.IsBusy = false;
        }
    }
}

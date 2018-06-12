using FitnessApp.Enums;
using FitnessApp.Interfaces;
using FitnessApp.Views.Meal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Meal
{
    public class EditMealViewModel : BaseViewModel
    {
        private Models.Meal selectedMeal;
        public Models.Meal SelectedMeal
        {
            get { return this.selectedMeal; }
            set { this.SetProperty<Models.Meal>(ref this.selectedMeal, value); }
        }

        private ObservableCollection<Models.Food> foodList;
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

        public ICommand SelectFoodCommand { get; private set; }

        public ICommand DeleteMealCommand { get; private set; }

        public ICommand UpdateMealCommand { get; private set; }

        public EditMealViewModel(Models.Meal meal)
        {
            this.SelectFoodCommand = new Command(this.SelectFood);
            this.DeleteMealCommand = new Command(this.DeleteMeal);
            this.UpdateMealCommand = new Command(this.UpdateMeal);
            this.Initialise(meal);
        }

        private async void Initialise(Models.Meal meal)
        {
            this.SelectedMeal = meal;
            this.FoodList = new ObservableCollection<Models.Food>();
            this.FoodList.Add(await this.LocalDatabaseFood.GetItemAsync(meal.FoodID));

            this.MealTypeIndex = (int)meal.MealType;
            this.MealTime = meal.DateTime.TimeOfDay;
            this.MealDate = meal.DateTime.Date;
            this.DescriptionText = meal.Description ?? "";
        }

        public void SelectFood()
        {
            MessagingCenter.Subscribe<SelectEditFoodViewModel, Models.Food>(this, "Select Food", (sender, arg) =>
            {
                this.FoodList.Clear();
                this.FoodList.Add(arg);
            });

            this.NavigationService.NavigateTo(typeof(SelectEditFoodPage));
        }

        private async void DeleteMeal()
        {
            if (this.IsBusy)
                return;

            this.IsBusy = true;

            await this.LocalDatabaseMeal.DeleteItemAsync(this.SelectedMeal);

            this.User.MealIsLoaded = false;
            this.NavigationService.GoBack();

            DependencyService.Get<IMessageHelper>().ShortAlert("Meal deleted.");

            this.IsBusy = false;
        }

        private async void UpdateMeal()
        {
            var message = DependencyService.Get<IMessageHelper>();

            if (this.IsBusy)
            {
                return;
            }
            this.IsBusy = true;

            this.SelectedMeal.FoodID = this.FoodList[0].ID ?? null;
            this.SelectedMeal.DateTime = new DateTime(
                                                this.MealDate.Year,
                                                this.MealDate.Month,
                                                this.MealDate.Day,
                                                this.MealTime.Hours,
                                                this.MealTime.Minutes,
                                                this.MealTime.Seconds);
            this.SelectedMeal.MealType = (MealType)this.MealTypeIndex;
            this.SelectedMeal.Description = this.DescriptionText ?? "";

            await this.LocalDatabaseMeal.SaveItemAsync(this.SelectedMeal);
            message.ShortAlert("Meal updated.");
            this.User.MealIsLoaded = false;
            this.NavigationService.GoBack();

            this.IsBusy = false;
        }
    }
}

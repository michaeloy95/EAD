using FitnessApp.Enums;
using FitnessApp.Interfaces;
using FitnessApp.Views.Food;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class AddFoodViewModel : BaseViewModel
    {
        private string nameText = string.Empty;
        public string NameText
        {
            get { return this.nameText; }
            set { this.SetProperty<string>(ref this.nameText, value); }
        }

        private string caloriesText = string.Empty;
        public string CaloriesText
        {
            get { return this.caloriesText; }
            set { this.SetProperty<string>(ref this.caloriesText, value); }
        }
        
        public IList<string> MeasurementItems
        {
            get { return new List<string>(Enum.GetNames(typeof(FoodMeasurement))); }
        }

        private int measurementIndex = 0;
        public int MeasurementIndex
        {
            get { return this.measurementIndex; }
            set { this.SetProperty<int>(ref this.measurementIndex, value); }
        }

        private string carbsText = string.Empty;
        public string CarbsText
        {
            get { return this.carbsText; }
            set { this.SetProperty<string>(ref this.carbsText, value); }
        }

        private string proteinText = string.Empty;
        public string ProteinText
        {
            get { return this.proteinText; }
            set { this.SetProperty<string>(ref this.proteinText, value); }
        }

        private string fatText = string.Empty;
        public string FatText
        {
            get { return this.fatText; }
            set { this.SetProperty<string>(ref this.fatText, value); }
        }

        public ICommand SearchFoodCommand { get; private set; }

        public ICommand AddFoodCommand { get; private set; }

        public AddFoodViewModel()
        {
            this.SearchFoodCommand = new Command(this.SearchFood);
            this.AddFoodCommand = new Command(this.AddFood);

            MessagingCenter.Subscribe<SearchFoodViewModel, Models.Food>(this, "Select Food", (sender, args) =>
            {
                this.UpdateFoodDetail(args);
            });
        }

        private void SearchFood()
        {
            this.NavigationService.NavigateTo(typeof(SearchFoodPage));
        }

        private void UpdateFoodDetail(Models.Food food)
        {
            this.NameText = food.Name;
            this.CaloriesText = $"{food.Calories}";
            this.CarbsText = $"{food.Carbs}";
            this.ProteinText = $"{food.Protein}";
            this.FatText = $"{food.Fat}";
            this.MeasurementIndex = (int)food.Measurement;
        }

        private async void AddFood()
        {
            var message = DependencyService.Get<IMessageHelper>();
            var field = (this.NameText == string.Empty) ? "Name"
                : (this.CaloriesText == string.Empty) ? "Calories"
                : (this.CarbsText == string.Empty) ? "Carbs"
                : (this.ProteinText == string.Empty) ? "Protein"
                : (this.FatText == string.Empty) ? "Fat"
                : string.Empty;

            if (field != string.Empty)
            {
                message.ShortAlert($"{field} cannot be empty.");
                return;
            }

            if (this.IsBusy)
            {
                return;
            }
            this.IsBusy = true;

            Models.Food food = new Models.Food()
            {
                ID = Guid.NewGuid().ToString(),
                Name = this.NameText,
                Measurement = (FoodMeasurement)this.MeasurementIndex,
                Calories = float.Parse(this.CaloriesText),
                Protein = float.Parse(this.ProteinText),
                Carbs = float.Parse(this.CarbsText),
                Fat = float.Parse(this.FatText),
                ImageSource = "imageplaceholder.png"
            };

            await this.LocalDatabaseFood.SaveItemAsync(food);
            this.NavigationService.SwitchTo(typeof(FoodIsAddedPage), new object[1] { food });
            this.User.FoodIsLoaded = false;

            this.IsBusy = false;
        }
    }
}

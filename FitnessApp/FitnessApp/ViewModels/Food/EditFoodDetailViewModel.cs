using FitnessApp.Enums;
using FitnessApp.Interfaces;
using FitnessApp.Views.Food;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class EditFoodDetailViewModel : BaseViewModel
    {
        private Models.Food selectedFood;
        public Models.Food SelectedFood
        {
            get { return this.selectedFood; }
            set { this.SetProperty<Models.Food>(ref this.selectedFood, value); }
        }

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

        public ICommand DoneCommand { get; private set; }

        public EditFoodDetailViewModel(Models.Food food)
        {
            this.SearchFoodCommand = new Command(this.SearchFood);
            this.DoneCommand = new Command(this.Done);

            this.Initialise(food);
        }

        private void Initialise(Models.Food food)
        {
            this.SelectedFood = food;
            this.NameText = food.Name;
            this.CaloriesText = food.Calories.ToString();
            this.CarbsText = food.Carbs.ToString();
            this.ProteinText = food.Protein.ToString();
            this.FatText = food.Fat.ToString();
            this.MeasurementIndex = (int)food.Measurement;

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

        private void Done()
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

            this.SelectedFood.Name = this.NameText;
            this.SelectedFood.Measurement = (FoodMeasurement)this.MeasurementIndex;
            this.SelectedFood.Calories = float.Parse(this.CaloriesText);
            this.SelectedFood.Protein = float.Parse(this.ProteinText);
            this.SelectedFood.Carbs = float.Parse(this.CarbsText);
            this.SelectedFood.Fat = float.Parse(this.FatText);

            MessagingCenter.Send<EditFoodDetailViewModel, Models.Food>(this, "Update Detail", this.SelectedFood);

            this.NavigationService.GoBack();
        }
    }
}

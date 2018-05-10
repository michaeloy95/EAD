using FitnessApp.Views.Food;
using Plugin.Media;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class FoodIsAddedViewModel : BaseViewModel
    {
        private Models.Food selectedFood;
        public Models.Food SelectedFood
        {
            get { return this.selectedFood; }
            set { SetProperty<Models.Food>(ref this.selectedFood, value); }
        }

        private string imageSource = "imageplaceholder.png";
        public string ImageSource
        {
            get { return this.imageSource; }
            set { SetProperty<string>(ref this.imageSource, value); }
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

        private string measurementText = string.Empty;
        public string MeasurementText
        {
            get { return this.measurementText; }
            set { this.SetProperty<string>(ref this.measurementText, value); }
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

        public ICommand UploadImageCommand { get; private set; }

        public ICommand AddMoreCommand { get; private set; }

        public ICommand DoneCommand { get; private set; }

        public FoodIsAddedViewModel(Models.Food food)
        {
            this.UploadImageCommand = new Command(this.UploadImage);
            this.AddMoreCommand = new Command(this.AddMore);
            this.DoneCommand = new Command(this.Done);

            this.InitialiseContent(food);
        }

        private void AddMore()
        {
            this.NavigationService.SwitchTo(typeof(AddFoodPage));
        }

        private void Done()
        {
            this.NavigationService.GoBack();
        }

        private void InitialiseContent(Models.Food food)
        {
            this.SelectedFood = food;
            this.NameText = food.Name;
            this.CaloriesText = $"{food.Calories}";
            this.MeasurementText = $"{food.MeasurementText}";
            this.CarbsText = $"{food.Carbs} g";
            this.ProteinText = $"{food.Protein} g";
            this.FatText = $"{food.Fat} g";
        }

        private async void UploadImage()
        {
            if (this.IsBusy)
                return;
            this.IsBusy = true;

            var media = CrossMedia.Current;
            var file = await media.PickPhotoAsync();

            if (file == null)
            {
                this.IsBusy = false;
                return;
            }

            this.SelectedFood.ImageSource = file.Path;
            this.ImageSource = file.Path;

            await this.LocalDatabaseFood.SaveItemAsync(this.SelectedFood);

            this.IsBusy = false;
        }
    }
}

using FitnessApp.Interfaces;
using FitnessApp.Views.Food;
using Plugin.Media;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class EditFoodViewModel : BaseViewModel
    {
        private Models.Food selectedFood;
        public Models.Food SelectedFood
        {
            get { return this.selectedFood; }
            set { SetProperty<Models.Food>(ref this.selectedFood, value); }
        }

        private string nameText = string.Empty;
        public string NameText
        {
            get { return this.nameText; }
            set { SetProperty<string>(ref this.nameText, value); }
        }

        private string imageSourceText = string.Empty;
        public string ImageSourceText
        {
            get { return this.imageSourceText; }
            set { SetProperty<string>(ref this.imageSourceText, value); }
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

        private bool isUpdated = false;
        public bool IsUpdated
        {
            get { return this.isUpdated; }
            set { this.SetProperty<bool>(ref this.isUpdated, value); }
        }

        public ICommand UploadImageCommand { get; private set; }

        public ICommand GotoFoodDetailCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand UpdateCommand { get; private set; }

        public EditFoodViewModel(Models.Food food)
        {
            this.UploadImageCommand = new Command(this.UploadImage);
            this.GotoFoodDetailCommand = new Command(this.GotoFoodDetail);
            this.DeleteCommand = new Command(this.DeleteFood);
            this.UpdateCommand = new Command(this.UpdateFood);

            this.InitialiseContent(food);
        }

        private void InitialiseContent(Models.Food food)
        {
            this.UpdateFoodDetail(food);

            MessagingCenter.Subscribe<EditFoodDetailViewModel, Models.Food>(this, "Update Detail", (sender, arg) =>
            {
                this.UpdateFoodDetail(arg);
            });
        }

        private void UpdateFoodDetail(Models.Food food)
        {
            this.SelectedFood = food;
            this.NameText = food.Name;
            this.ImageSourceText = food.ImageSource;
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

            this.IsUpdated = true;
            this.SelectedFood.ImageSource = file.Path;
            this.ImageSourceText = file.Path;

            this.IsBusy = false;
        }

        private void GotoFoodDetail()
        {
            this.NavigationService.NavigateTo(typeof(EditFoodDetailPage), new object[1] { this.SelectedFood });
            this.IsUpdated = true;
        }

        private async void DeleteFood()
        {
            if (this.IsBusy)
                return;

            this.IsBusy = true;

            MessagingCenter.Unsubscribe<EditFoodViewModel, Models.Food>(this, "Update Detail");

            await this.LocalDatabaseFood.DeleteItemAsync(this.SelectedFood);

            this.User.FoodIsLoaded = false;
            this.NavigationService.GoBack();

            DependencyService.Get<IMessageHelper>().ShortAlert("Food is deleted.");

            this.IsBusy = false;
        }

        private async void UpdateFood()
        {
            if (this.IsBusy)
                return;

            this.IsBusy = true;

            MessagingCenter.Unsubscribe<EditFoodViewModel, Models.Food>(this, "Update Detail");

            await this.LocalDatabaseFood.SaveItemAsync(this.SelectedFood);

            this.User.FoodIsLoaded = false;
            this.NavigationService.GoBack();

            DependencyService.Get<IMessageHelper>().ShortAlert("Food is successfully updated.");

            this.IsBusy = false;
        }
    }
}

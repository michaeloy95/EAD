using FitnessApp.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFoodPage : ContentPage
    {
        public EditFoodViewModel ViewModel;

        public EditFoodPage(Models.Food food)
        {
            InitializeComponent();

            this.ViewModel = new EditFoodViewModel(food)
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }

        private void FoodImage_Tapped(object sender, System.EventArgs e)
        {
            if (this.ViewModel.UploadImageCommand.CanExecute(null))
            {
                this.ViewModel.UploadImageCommand.Execute(null);
            }
        }

        private void FoodDetail_Tapped(object sender, System.EventArgs e)
        {
            if (this.ViewModel.GotoFoodDetailCommand.CanExecute(null))
            {
                this.ViewModel.GotoFoodDetailCommand.Execute(null);
            }
        }
    }
}
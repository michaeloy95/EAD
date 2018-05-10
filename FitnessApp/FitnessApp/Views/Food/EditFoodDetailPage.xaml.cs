using FitnessApp.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFoodDetailPage : ContentPage
    {
        public EditFoodDetailViewModel ViewModel;

        public EditFoodDetailPage(Models.Food food)
        {
            InitializeComponent();

            this.ViewModel = new EditFoodDetailViewModel(food)
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }
    }
}
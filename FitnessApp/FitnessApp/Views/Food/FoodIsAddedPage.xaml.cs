using FitnessApp.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodIsAddedPage : ContentPage
	{
        public FoodIsAddedViewModel ViewModel;

		public FoodIsAddedPage ()
		{
			InitializeComponent ();

            this.ViewModel = new FoodIsAddedViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }
	}
}
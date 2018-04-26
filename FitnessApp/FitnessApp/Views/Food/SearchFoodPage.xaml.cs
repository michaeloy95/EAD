using FitnessApp.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchFoodPage : ContentPage
	{
        public SearchFoodViewModel ViewModel;

		public SearchFoodPage ()
		{
			InitializeComponent ();

            this.ViewModel = new SearchFoodViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }

        private void FoodListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.MyFoodListView.SelectedItem = null;
        }
    }
}
using FitnessApp.ViewModels.Meal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Meal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectFoodPage : ContentPage
	{
        public SelectFoodViewModel ViewModel;

		public SelectFoodPage ()
		{
			InitializeComponent ();

            this.ViewModel = new SelectFoodViewModel()
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
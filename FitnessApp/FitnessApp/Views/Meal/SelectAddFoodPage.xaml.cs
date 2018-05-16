using FitnessApp.ViewModels.Meal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Meal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectAddFoodPage : ContentPage
	{
        public SelectAddFoodViewModel ViewModel;

		public SelectAddFoodPage ()
		{
			InitializeComponent ();

            this.ViewModel = new SelectAddFoodViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }

        private void FoodListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            if (this.ViewModel.SelectFoodCommand.CanExecute(e.SelectedItem))
            {
                this.ViewModel.SelectFoodCommand.Execute(e.SelectedItem);
            }

            this.MyFoodListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            this.ViewModel.RefreshItems();
        }
    }
}
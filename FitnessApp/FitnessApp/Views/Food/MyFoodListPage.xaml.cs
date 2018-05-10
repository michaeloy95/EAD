using FitnessApp.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFoodListPage : ContentPage
    {
        public MyFoodListViewModel ViewModel;

        public MyFoodListPage()
        {
            InitializeComponent();

            this.ViewModel = new MyFoodListViewModel()
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

            if (this.ViewModel.EditCommand.CanExecute(e.SelectedItem))
            {
                this.ViewModel.EditCommand.Execute(e.SelectedItem);
            }

            this.MyFoodListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.ViewModel.RefreshItem();
        }
    }
}
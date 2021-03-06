﻿using FitnessApp.ViewModels.Food;
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
    }
}
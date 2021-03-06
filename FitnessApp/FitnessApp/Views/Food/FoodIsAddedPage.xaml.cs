﻿using FitnessApp.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodIsAddedPage : ContentPage
	{
        public FoodIsAddedViewModel ViewModel;

		public FoodIsAddedPage(Models.Food food)
		{
			InitializeComponent ();

            this.ViewModel = new FoodIsAddedViewModel(food)
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
    }
}
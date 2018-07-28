using FitnessApp.Views.Meal;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private Interfaces.IPedometerHelper pedometerHelper;
        private ObservableCollection<Models.Meal> mealList;
        public ObservableCollection<Models.Meal> MealList
        {
            get { return this.mealList; }
            set { this.SetProperty<ObservableCollection<Models.Meal>>(ref this.mealList, value); }
        }

        private bool mealListIsEmpty;
        public bool MealListIsEmpty
        {
            get { return this.mealListIsEmpty; }
            set
            {
                this.SetProperty<bool>(ref this.mealListIsEmpty, value);
                this.MealListIsNotEmpty = !this.mealListIsEmpty;
            }
        }

        private bool mealListIsNotEmpty;
        public bool MealListIsNotEmpty
        {
            get { return this.mealListIsNotEmpty; }
            set { this.SetProperty<bool>(ref this.mealListIsNotEmpty, value); }
        }

        public ICommand AddMealCommand { get; private set; }

        public ICommand SelectMealCommand { get; private set; }

        public ProfileViewModel()
        {
            this.AddMealCommand = new Command(this.AddMeal);
            this.SelectMealCommand = new Command<Models.Meal>(this.SelectMeal);

            this.RefreshItem();
            
        }
        

        public async void RefreshItem()
        {
            this.MealList = new ObservableCollection<Models.Meal>(await this.LocalDatabaseMeal.GetItemsAsync());
            this.MealListIsEmpty = this.MealList == null || this.MealList.Count == 0;
            var deviceUtil = DependencyService.Get<Interfaces.IDeviceUtility>();
            if (deviceUtil.AndroidStepSupport)
            {
                var message = DependencyService.Get<Interfaces.IMessageHelper>();
                var pedometer = DependencyService.Get<Interfaces.IPedometerHelper>();
                //message.ShortAlert(pedometer.GetTotalStep().ToString());
                var stepsToday = Helpers.Settings.StepsToday.ToString();
                string getStepsCountToDate = "";
                if (Helpers.Settings.GetStepsCountToDate(DateTime.Now.ToString("d")) == null)
                    getStepsCountToDate = 0.ToString();
                else
                    getStepsCountToDate = Helpers.Settings.GetStepsCountToDate(DateTime.Now.ToString("d")).Skip(1).Sum(x => x.Value).ToString();
                message.ShortAlert(DateTime.Now.ToString("d")+": "+
                    stepsToday+" steps; Total: " + getStepsCountToDate + " steps");
            }
        }

        private void AddMeal()
        {
            this.NavigationService.NavigateTo(typeof(SelectAddFoodPage));
        }

        private void SelectMeal(Models.Meal meal)
        {
            this.NavigationService.NavigateTo(typeof(EditMealPage), new object[1] { meal });
        }
    }
}
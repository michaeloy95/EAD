using FitnessApp.Views.Meal;
using FitnessApp.Views.Profile;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private string profileImageSource = "";
        public string ProfileImageSource
        {
            get { return this.profileImageSource; }
            set { this.SetProperty<string>(ref this.profileImageSource, value); }
        }

        private string nameText = "";
        public string NameText
        {
            get { return this.nameText; }
            set { this.SetProperty<string>(ref this.nameText, value); }
        }

        private string bioText = "";
        public string BioText
        {
            get { return this.bioText; }
            set { this.SetProperty<string>(ref this.bioText, value); }
        }

        private string weightText = "";
        public string WeightText
        {
            get { return this.weightText; }
            set { this.SetProperty<string>(ref this.weightText, value); }
        }

        private string bmiText = "";
        public string BmiText
        {
            get { return this.bmiText; }
            set { this.SetProperty<string>(ref this.bmiText, value); }
        }

        private string rdiText = "";
        public string RdiText
        {
            get { return this.rdiText; }
            set { this.SetProperty<string>(ref this.rdiText, value); }
        }

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

        public ICommand EditProfileCommand { get; private set; }

        public ICommand AddMealCommand { get; private set; }

        public ICommand SelectMealCommand { get; private set; }

        public ProfileViewModel()
        {
            this.EditProfileCommand = new Command(this.EditProfile);
            this.AddMealCommand = new Command(this.AddMeal);
            this.SelectMealCommand = new Command<Models.Meal>(this.SelectMeal);

            this.RefreshItem();
        }

        public async void RefreshItem()
        {
            this.NameText = this.User.Name;
            this.ProfileImageSource = this.User.ImageSource;
            int ageTmp = DateTime.Today.Year - this.User.Birthdate.Year;
            ageTmp -= (this.User.Birthdate > DateTime.Today.AddYears(-ageTmp)) ? 1 : 0;
            this.BioText = $"{ageTmp} years / {this.User.Height} cm";
            this.WeightText = this.User.Weight.ToString();
            this.BmiText = this.User.BMI.ToString();
            this.RdiText = this.User.RDI.ToString();

            this.MealList = new ObservableCollection<Models.Meal>(await this.LocalDatabaseMeal.GetItemsAsync());
            this.MealListIsEmpty = this.MealList == null || this.MealList.Count == 0;
        }

        private void EditProfile()
        {
            this.NavigationService.NavigateTo(typeof(EditProfilePage));
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
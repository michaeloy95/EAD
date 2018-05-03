using FitnessApp.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Food
{
    public class SearchFoodViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Food> foodList;
        public ObservableCollection<Models.Food> FoodList
        {
            get { return this.foodList; }
            set { this.SetProperty<ObservableCollection<Models.Food>>(ref this.foodList, value); }
        }

        private string searchEntryText = string.Empty;
        public string SearchEntryText
        {
            get { return this.searchEntryText; }
            set { SetProperty<string>(ref this.searchEntryText, value); }
        }

        public ICommand SearchCommand { get; private set; }

        public SearchFoodViewModel()
        {
            this.SearchCommand = new Command(this.SearchFood);
        }

        private async void SearchFood()
        {
            var foods = await FatSecretPlatformAPIService.GetFoods(this.SearchEntryText);
            foodList = new ObservableCollection<Models.Food>();
            JArray foodsRetrieved = JArray.Parse(foods["food"].ToString());
            Models.Food temp;
            foreach (var aFood in foodsRetrieved)
            {
               

                var initSplit = aFood["food_description"].ToString().Split('-');
                var nutritionString = initSplit[1].Split('|');

                Dictionary<string, float> nutritionDict = new Dictionary<string, float>();
                foreach (string aNutrition in nutritionString)
                {
                    var split = aNutrition.Split(':');

                    string tempNutrition = "";
                    foreach (char aChar in tempNutrition)
                    {
                        if (aChar != ' ' || aChar != 'k' || aChar != 'g')
                            tempNutrition += aChar;
                        else if (aChar == 'k' || aChar == 'g')
                            break;
                    }

                    nutritionDict.Add(split[0].Trim(), float.Parse(tempNutrition));
                }

                temp = new Models.Food();
                temp.ID = aFood["food_id"].ToString();
                temp.Name = aFood["food_name"].ToString();
                temp.ImageSource = aFood["food_url"].ToString();
                temp.Calories = nutritionDict["Calories"];
                temp.Fat = nutritionDict["Fat"];
                temp.Protein = nutritionDict["Protein"];
                temp.Hydrates = nutritionDict["Carbs"];
                //temp.Weight = initSplit[0];

                foodList.Add(temp);
            }
        }
    }
}

using FitnessApp.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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

        private string searchEntryText;
        public string SearchEntryText
        {
            get { return this.searchEntryText; }
            set
            {
                SetProperty<string>(ref this.searchEntryText, value);
                if (this.searchEntryText != string.Empty)
                {
                   var task = Task.Run(async () =>
                    {
                        await SearchFood();
                    });
                    task.Wait();
                }
            }
        }

        public SearchFoodViewModel()
        {
            this.searchEntryText = string.Empty;
            this.foodList = new ObservableCollection<Models.Food>();
        }

        private async Task SearchFood()
        {
            var foodResp = await FatSecretPlatformAPIService.GetFoods(this.SearchEntryText);
            foodList.Clear();
            if (foodResp != null)
            {
                var foods = foodResp["foods"]["food"];
                JArray foodsRetrieved = JArray.Parse(foods.ToString());
                Models.Food temp;
                foreach (JObject aFood in foodsRetrieved)
                {
                    temp = new Models.Food();

                    temp.ParseJObject(aFood);

                    foodList.Add(temp);
                }
            }
        }
    }
}

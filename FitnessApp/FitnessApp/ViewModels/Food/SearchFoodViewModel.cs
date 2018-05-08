using FitnessApp.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Connectivity;
using System;

namespace FitnessApp.ViewModels.Food
{
    public class SearchFoodViewModel : BaseViewModel
    {
        // <summary>
        // Dictionary to cache search data
        // key : search term
        // value : food list result
        // </summary>
        private Dictionary<string, ObservableCollection<Models.Food>> cacheData;

        private bool stopTyping = false;

        private Task myTask;

        private CancellationTokenSource cts;

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
                    stopTyping = false;
                    if (myTask != null)
                        if (myTask.Status == TaskStatus.Running)
                            cts.Cancel();
                        
                    Device.StartTimer(System.TimeSpan.FromMilliseconds(500), () => 
                    {
                        myTask = Task.Factory.StartNew(async () =>
                        {
                            stopTyping = true;
                            if (stopTyping)
                                if (CrossConnectivity.Current.IsConnected)
                                    await SearchFood();
                                else
                                {
                                    // No internet connection
                                }
                        });
                        return false;
                    });
                }
            }
        }

        public SearchFoodViewModel()
        {
            this.cacheData = new Dictionary<string, ObservableCollection<Models.Food>>();
            this.searchEntryText = string.Empty;
            this.foodList = new ObservableCollection<Models.Food>();
            this.cts = new CancellationTokenSource();
        }

        private async Task SearchFood()
        {
            // if data exist in cache, take from there instead of requesting to API
            if (cacheData.ContainsKey(this.searchEntryText))
            {
                this.FoodList = cacheData[this.searchEntryText];
            }
            else
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
                cacheData.Add(this.searchEntryText, new ObservableCollection<Models.Food>(this.foodList));
            }
        }
    }
}

using FitnessApp.Enums;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FitnessApp.Models
{
    public class Food
    {
        [PrimaryKey]
        public string ID { get; set; }
        
        public string Name { get; set; }

        public float Calories { get; set; }

        public FoodMeasurement Measurement { get; set; }

        public string MeasurementText
        {
            get
            {
                return (this.Measurement == FoodMeasurement.HundredGrams) ? "100 grams"
                : (this.Measurement == FoodMeasurement.Package) ? "1 package"
                : string.Empty;
            }
        }

        public float Protein { get; set; }

        public float Carbs { get; set; }

        public float Fat { get; set; }

        public string ImageSource { get; set; }

        // <summary>
        // Read-only property to display Description
        // </summary>
        public string Description
        {
            get
            {
                return Measurement.ToString() + " - Calories: " + Calories.ToString() +
                    "g | Fat: " + Fat.ToString() + "g | Carbs: " + Carbs.ToString() + "g | Protein: " + Protein.ToString();
            }
        }

        // <summary>
        // Function to parse data from JObject taken from FatSecret API
        // </summary>
        public Food ParseJObject(JObject aFood)
        {
            try
            {
                var initSplit = aFood["food_description"].ToString().Split('-');
                var nutritionString = initSplit[1].Split('|');

                Dictionary<string, float> nutritionDict = new Dictionary<string, float>();
                foreach (string aNutrition in nutritionString)
                {
                    var split = aNutrition.Split(':');

                    string tempNutrition = "";
                    foreach (char aChar in split[1])
                    {
                        if (!char.IsWhiteSpace(aChar) && aChar != 'k' && aChar != 'g')
                            tempNutrition += aChar;
                        else if (aChar == 'k' || aChar == 'g')
                            break;
                    }

                    nutritionDict.Add(split[0].Trim(), float.Parse(tempNutrition));
                }

                string tempWeight = "", tempMeasurement = "";
                foreach (char aChar in initSplit[0].Substring(4))
                {
                    if (char.IsLetter(aChar))
                        tempMeasurement += aChar;
                    else if (!char.IsWhiteSpace(aChar))
                        tempWeight += aChar;
                }

                this.ID = aFood["food_id"].ToString();
                this.Name = aFood["food_name"].ToString();
                this.ImageSource = aFood["food_url"].ToString();
                this.Calories = nutritionDict["Calories"];
                this.Fat = nutritionDict["Fat"];
                this.Protein = nutritionDict["Protein"];
                this.Carbs = nutritionDict["Carbs"];
                //this.Weight = float.Parse(tempWeight);
                //this.Measurement = tempMeasurement;
                if (tempMeasurement == "g")
                    this.Measurement = FoodMeasurement.HundredGrams;
                else
                    this.Measurement = FoodMeasurement.Package;
                return this;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

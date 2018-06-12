using FitnessApp.Enums;
using SQLite;
using System;

namespace FitnessApp.Models
{
    public class Meal
    {
        [PrimaryKey]
        public string ID { get; set; }

        public string FoodID { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public MealType MealType { get; set; }

        public string MealTypeString
        {
            get
            {
                return this.MealType == MealType.Breakfast ? "Breakfast"
                    : this.MealType == MealType.Brunch ? "Brunch"
                    : this.MealType == MealType.Lunch ? "Lunch"
                    : this.MealType == MealType.Dinner ? "Dinner"
                    : this.MealType == MealType.Supper ? "Supper"
                    : "Unknown";
            }
        }
    }
}

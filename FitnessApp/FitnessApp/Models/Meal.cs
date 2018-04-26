using FitnessApp.Enums;
using System;

namespace FitnessApp.Models
{
    public class Meal
    {
        public string ID { get; set; }

        public Food Food { get; set; }

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

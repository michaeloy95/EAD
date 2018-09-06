using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FitnessApp.Interfaces;

namespace FitnessApp.Droid.Pedometer
{
    public class PedometerHelper : IPedometerHelper
    {
        private static Int64 totalStep;
        public static Int64 TotalStep
        {
            get
            {
                return totalStep;
            }
            set
            {
                if (totalStep == value)
                    return;
                totalStep = value;
            }
        }
       

        public long GetTotalStep()
        {
            return TotalStep;
        }
    }
}
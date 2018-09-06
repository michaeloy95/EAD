using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FitnessApp.Droid.Pedometer
{
    public class PedometerBinder : Binder
    {
        PedometerService ps;
        public PedometerBinder(PedometerService ps)
        {
            this.ps = ps;
        }
        public PedometerService PedometerService
        {
            get { return ps; }
        }
    }
}
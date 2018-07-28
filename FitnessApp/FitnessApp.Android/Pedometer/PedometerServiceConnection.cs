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
    public class PedometerServiceConnection : Java.Lang.Object, IServiceConnection
    {
        MainActivity mainActivity;

        public PedometerServiceConnection(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var serviceBinder = service as PedometerBinder;
            if (serviceBinder != null)
            {
                mainActivity.Binder = serviceBinder;
                mainActivity.IsBound = true;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            mainActivity.IsBound = false;
        }
    }
}
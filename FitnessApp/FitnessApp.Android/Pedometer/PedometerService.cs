using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FitnessApp.Interfaces;
using Plugin.Settings;

namespace FitnessApp.Droid.Pedometer
{
    [Service(Enabled = true)]
    [IntentFilter(new String[] { "au.com.franksons.ExerciseDietApp.PedometerService" })]
    public class PedometerService : Service, ISensorEventListener, INotifyPropertyChanged
    {

        private Int64 newSteps = 0;
        public Int64 NewSteps
        {
            get { return newSteps; }
            set
            {
                if (newSteps == value)
                    return;

                newSteps = value;
                OnPropertyChanged("NewSteps");
                //Update step entry data on the file
                FitnessApp.Helpers.Settings.StepsToday = (long)FitnessApp.Helpers.Settings.StepsToday + (long)value;
            }
        }

        private Int64 prevSteps = 0;
        private bool isRunning = false;
        PedometerBinder binder;
        
        public bool WarningState
        {
            get;
            set;
        }


        public override void OnCreate()
        {
            base.OnCreate();

            var util = new Utilities.DeviceUtility();

            if (isRunning || !util.IsKitKatWithStepCounter(PackageManager))
                return;

            var sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            var sensor = sensorManager.GetDefaultSensor(SensorType.StepCounter);

            sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);
            Console.WriteLine("Sensor listener registered of type: " + SensorType.StepCounter);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            var sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            sensorManager.UnregisterListener(this);
            Console.WriteLine("Sensor ("+ SensorType.StepCounter + ") listener unregistered.");

            isRunning = false;
        }

        public override IBinder OnBind(Intent intent)
        {
            binder = new PedometerBinder(this);
            return binder;
        }

        public void OnSensorChanged(SensorEvent e)
        {
            switch (e.Sensor.Type)
            {
                //just use stepcounter (more accurate than stepdetector)
                case SensorType.StepCounter:
                    var sensorCount = (Int64)e.Values[0];

                    //store starting date of the step data
                    FitnessApp.Helpers.Settings.StartDate = DateTime.Now;
                    if (prevSteps == 0)
                    {
                        prevSteps = sensorCount;
                        PedometerHelper.TotalStep = sensorCount;
                    }
                    if (sensorCount >= 0)
                    {
                        NewSteps = sensorCount - prevSteps;
                        prevSteps = sensorCount;
                        PedometerHelper.TotalStep = sensorCount;
                    }
                    if (sensorCount < 0)
                    {
                        NewSteps = 1;
                        PedometerHelper.TotalStep += 1;
                        //error
                        if (!isRunning)
                            return;
                        else
                        {
                            //restart sensor
                            var sensorManager = (SensorManager)GetSystemService(Context.SensorService);
                            sensorManager.UnregisterListener(this);
                            Console.WriteLine("Sensor listener unregistered");

                            var sensor = sensorManager.GetDefaultSensor(SensorType.StepCounter);
                            sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);
                            Console.WriteLine("Sensor listener registered of type: " + SensorType.StepCounter);

                        }
                    }
                    break;
            }
        }
        public void OnAccuracyChanged(Sensor s, SensorStatus ss)
        {
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Console.WriteLine("start command");
#if DEBUG
            Android.Util.Log.Debug("PEDOMETERSERVICE", "Start command result called, incoming startup");
#endif
            var alarmManager = ((AlarmManager)ApplicationContext.GetSystemService(Context.AlarmService));
            var intent2 = new Intent(this, typeof(PedometerService));
            intent2.PutExtra("warning", WarningState);
            var stepIntent = PendingIntent.GetService(ApplicationContext, 10, intent2, PendingIntentFlags.UpdateCurrent);
            alarmManager.Set(AlarmType.Rtc, Java.Lang.JavaSystem
                .CurrentTimeMillis() + 1000 * 60 * 60, stepIntent);

            var warning = false;
            if (intent != null)
                warning = intent.GetBooleanExtra("warning", false);

            return StartCommandResult.Sticky;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Autofac;
using CarouselView.FormsPlugin.Android;
using FitnessApp.Droid.Pedometer;
using FitnessApp.Utilities;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using System;

namespace FitnessApp.Droid
{
    [Activity(Label = "Exercise Diet App", 
              Icon = "@drawable/icon", 
              Theme = "@style/MyTheme", 
              MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private bool firstRun = true;
        private bool registered;
        private PedometerServiceConnection serviceConnection;
        private Handler handler;


        public bool IsBound { get; set; }
        private Pedometer.PedometerBinder binder;
        public Pedometer.PedometerBinder Binder
        {
            get { return binder; }
            set
            {
                binder = value;
                if (binder == null)
                    return;

                HandlePropertyChanged(null, new System.ComponentModel.PropertyChangedEventArgs("NewSteps"));

                if (registered)
                    binder.PedometerService.PropertyChanged -= HandlePropertyChanged;

                binder.PedometerService.PropertyChanged += HandlePropertyChanged;
                registered = true;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            CarouselViewRenderer.Init();
            //UserDialogs.Init(this);

            RegisterServices();
            StartPedometerService();

            LoadApplication(new App());
        }

        protected override void OnStart()
        {
            base.OnStart();
            var util = new Utilities.DeviceUtility();
            var sensorSupport = util.IsKitKatWithStepCounter(PackageManager);
            if (!sensorSupport)
            {
                Console.WriteLine("Not compatible with sensors, stopping service.");
                return;
            }
            if (!firstRun)
            {
                StartPedometerService();
            }
            if (IsBound)
                return;

            var serviceIntent = new Intent(this, typeof(PedometerService));
            serviceConnection = new PedometerServiceConnection(this);
            BindService(serviceIntent, serviceConnection, Bind.AutoCreate);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (IsBound)
            {
                UnbindService(serviceConnection);
                IsBound = false;
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            if (!firstRun)
            {

                if (handler == null)
                    handler = new Handler();
                //handler.PostDelayed(() => UpdateUI(true), 500);
            }

            firstRun = false;
            if (!registered && binder != null)
            {
                binder.PedometerService.PropertyChanged += HandlePropertyChanged;
                registered = true;
            }
        }
        protected override void OnPause()
        {
            base.OnPause();
            if (registered && binder != null)
            {
                binder.PedometerService.PropertyChanged -= HandlePropertyChanged;
                registered = false;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
        
        private static void RegisterServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<PlatformModule>();
            ViewModelLocator.RegisterServices(builder);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void StartPedometerService()
        {
            try
            {
                var service = new Intent(this, typeof(Pedometer.PedometerService));
                var componentName = StartService(service);
            }
            catch (Exception)
            {
            }
        }

        void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "NewSteps")
                return;
            //var msgHelper = new Helpers.MessageHelper();
            //RunOnUiThread(() => msgHelper.LongAlert("NewSteps property changed"));
        }
    }
}
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Autofac;
using CarouselView.FormsPlugin.Android;
using FitnessApp.Utilities;
using Plugin.CurrentActivity;
using Plugin.Permissions;

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

            LoadApplication(new App());
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
    }
}
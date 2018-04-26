using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Autofac;
using CarouselView.FormsPlugin.Android;
using FitnessApp.Utilities;

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
            CarouselViewRenderer.Init();

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
    }
}
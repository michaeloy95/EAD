using System;
using System.IO;
using FitnessApp.Interfaces;
using System.Reflection;
using Android.App;
using System.Globalization;
using Android.Util;
using System.Text;
using Android.Content;
using System.Threading.Tasks;
using Plugin.CurrentActivity;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Android.OS;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FitnessApp.Droid.Utilities
{
    public class DeviceUtility : IDeviceUtility
    {
        /// <summary>
        /// Request codes.
        /// </summary>
        public enum RequestCodes
        {
            /// <summary>
            /// The expoert log file.
            /// </summary>
            ExportLogFile = 1024,
        }

        /// <summary>
        /// The Android device log message tag.
        /// </summary>
        public static readonly string DebugTag = "Fitness App";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceUtility"/> class.
        /// </summary>
        public DeviceUtility()
        {
        }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        public string DeviceName
        {
            get
            {
                return Android.Bluetooth.BluetoothAdapter.DefaultAdapter.Name;
            }
        }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        public string AppName
        {
            get
            {
                var attrs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attrs.Length == 0
                    ? string.Empty
                        : ((AssemblyProductAttribute)attrs[0]).Product;
            }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        public string AppVersion
        {
            get
            {
                var context = Application.Context;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            }
        }

        /// <summary>
        /// Gets the file version.
        /// </summary>
        public string FileVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the build version.
        /// </summary>
        public string BuildVersion
        {
            get
            {
                var context = Application.Context;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionCode.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the copyright string.
        /// </summary>
        public string Copyright
        {
            get
            {
                var attrs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attrs.Length == 0
                    ? string.Empty
                        : ((AssemblyCopyrightAttribute)attrs[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the current application path.
        /// </summary>
        public string AppPath
        {
            get
            {
                return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            }
        }

        /// <summary>
        /// Gets the current device status bar height.
        /// </summary>
        public double StatusBarHeight
        {
            get
            {
                var context = Application.Context;
                var resourceId = context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
                if (resourceId > 0)
                {
                    return context.Resources.GetDimensionPixelSize(resourceId) / this.DisplayScale;
                }

                return 20.0;
            }
        }

        /// <summary>
        /// Gets the current device action bar height.
        /// </summary>
        private double ActionBarHeight
        {
            get
            {
                var context = Application.Context;
                var typedValue = new TypedValue();
                if (context.Theme.ResolveAttribute(Android.Resource.Attribute.ActionBarSize, typedValue, true))
                {
                    return TypedValue.ComplexToDimensionPixelSize(typedValue.Data, context.Resources.DisplayMetrics);
                }

                return 0.0;
            }
        }

        /// <summary>
        /// Gets the display scale.
        /// </summary>
        /// <value>The display scale.</value>
        private double DisplayScale
        {
            get
            {
                return Application.Context.Resources.DisplayMetrics.ScaledDensity;
            }
        }

        /// <summary>
        /// Gets the log file name full path.
        /// </summary>
        /// <value>The log file name full path.</value>
        private string LogFileName
        {
            get
            {
                return Path.Combine(this.AppPath, "FitnessApp.log");
            }
        }

        /// <summary>
        /// Gets if device can vibrate
        /// </summary>
        public bool CanVibrate
        {
            get
            {
                if ((int)Android.OS.Build.VERSION.SdkInt >= 11)
                {
                    using (var vibrator = (Android.OS.Vibrator)Android.App.Application.Context.GetSystemService(Context.VibratorService))
                    {
                        return vibrator.HasVibrator;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Capture current active screen.
        /// </summary>
        /// <returns>The captured active screen image filename.</returns>
        public string CaptureScreen()
        {
            return string.Empty;
        }

        /// <summary>
        /// Writes the log file.
        /// </summary>
        /// <param name="tag">Tag.</param>
        /// <param name="message">Message.</param>
        public void WriteLogFile(string tag, string message)
        {
            try
            {
                using (var writer = File.Open(this.LogFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (writer.CanSeek)
                    {
                        writer.Seek(0, SeekOrigin.End);
                    }

                    var contents = Encoding.UTF8.GetBytes($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {tag} - {message}\r\n");
                    writer.Write(contents, 0, contents.Length);
                }
            }
            catch (IOException ex)
            {
                Log.Error(DebugTag, ex.Message);
            }
        }

        /// <summary>
        /// Opens the log file.
        /// </summary>
        public void OpenLogFile()
        {
            if (!this.HasLogFile())
            {
                return;
            }

            /*
                due to Android security, and the app folder is not shared
                therefore, we need to export the log file to external
                storage (SD Card) first.
            */

            // permission check and enquiry only apply to Android API 23+
            if ((int)Android.OS.Build.VERSION.SdkInt >= 23)
            {
                Task.Run(async () =>
                {
                    var permission = await this.CheckPermission();
                    if (permission != PermissionStatus.Granted)
                    {
                        return;
                    }

                    this.DoOpenLogFile();
                    return;
                });

                return;
            }

            this.DoOpenLogFile();
        }

        /// <summary>
        /// Deletes the log file.
        /// </summary>
        public void DeleteLogFile()
        {
            if (!File.Exists(this.LogFileName))
            {
                return;
            }

            File.Delete(this.LogFileName);
        }

        /// <summary>
        /// Exports the log file.
        /// </summary>
        public void ExportLogFile()
        {
            if (!this.HasLogFile())
            {
                return;
            }

            var exportFileName = $"LE{DateTime.Now:yyyyMMdd-HHmmss}.log";
            var intent = new Intent(Intent.ActionCreateDocument);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("application/octet-stream");
            intent.PutExtra(Intent.ExtraTitle, exportFileName);
            CrossCurrentActivity.Current.Activity.StartActivityForResult(intent, (int)RequestCodes.ExportLogFile);
        }

        /// <summary>
        /// Exports the log file.
        /// </summary>
        /// <param name="output">The output I/O stream.</param>
        public void ExportLogFile(Stream output)
        {
            try
            {
                if (!this.HasLogFile() || output == null || !output.CanWrite)
                {
                    return;
                }

                if (output.CanSeek)
                {
                    output.SetLength(0);
                }

                using (var input = File.Open(this.LogFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var contents = new byte[1024];
                    int read = 0;

                    while ((read = input.Read(contents, 0, contents.Length)) > 0)
                    {
                        output.Write(contents, 0, read);
                    }
                }
            }
            catch (Exception ex)
            {
                var page = Xamarin.Forms.Application.Current.MainPage;
                page.DisplayAlert("Fitness App", $"Error: {ex.ToString()}", "OK");
            }
        }

        /// <summary>
        /// Reports the error to raygun.
        /// </summary>
        /// <param name="action">Action.</param>
        /// <param name="message">Message.</param>
        /// <param name="message">Error message.</param>
        /// <param name="data">Data.</param>
        public void ReportErrorToRaygun(string action, string message, string errorMessage, object data)
        {
#if !DEBUG
            try
            {
                var info = new Dictionary<string, string>();
                info.Add("Action", action);
                info.Add("Message", message);
                info.Add("Error Message", errorMessage);
                info.Add("Data", JsonConvert.SerializeObject(data));

               // RaygunClient.Current?.SendInBackground(new Exception("LE Clerk metric tracking exception."), new[] { "Metric", "Tracking" }, info);
            }
            catch (Exception ex)
            {
               // RaygunClient.Current?.SendInBackground(ex);
            }
#endif
        }

        /// <summary>
        /// Hases the log file.
        /// </summary>
        /// <returns><c>true</c>, if log file was hased, <c>false</c> otherwise.</returns>
        private bool HasLogFile()
        {
            if (!File.Exists(this.LogFileName))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Dos the open log file.
        /// </summary>
        private void DoOpenLogFile()
        {
            var target = string.Empty;
            if (Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
            {
                var folder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "ITSG");
                this.CreateStorageDirectory(folder);

                target = Path.Combine(folder, "LeClerk.log");
                this.CopyFile(this.LogFileName, target);
            }

            var activity = CrossCurrentActivity.Current.Activity;
            var file = new Java.IO.File(target);
            var intent = new Intent(Intent.ActionView);

            intent.SetDataAndType(Android.Net.Uri.FromFile(file), "text/plain");
            activity.StartActivity(intent);
        }

        /// <summary>
        /// Copies the file.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="target">Target.</param>
        private void CopyFile(string source, string target)
        {
            if (!File.Exists(source))
            {
                return;
            }

            using (var input = File.Open(source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var output = File.Open(target, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                try
                {
                    if (output.CanSeek)
                    {
                        output.SetLength(0);
                    }

                    var contents = new byte[1024];
                    int read = 0;

                    while ((read = input.Read(contents, 0, contents.Length)) > 0)
                    {
                        output.Write(contents, 0, read);
                    }
                }
                catch (IOException ex)
                {
                    var page = Xamarin.Forms.Application.Current.MainPage;
                    page.DisplayAlert("Fitness App", $"Error: {ex.ToString()}", "OK");
                }
            }
        }

        /// <summary>
        /// Creates the storage directory.
        /// </summary>
        /// <param name="path">Path.</param>
        private void CreateStorageDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            string[] paths = path.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            if (paths.Length == 0)
            {
                return;
            }

            string folder = Path.Combine(Path.DirectorySeparatorChar.ToString(), paths[0]);
            for (int index = 1; index < paths.Length; index++)
            {
                folder = Path.Combine(folder, paths[index]);

                if (Directory.Exists(folder))
                {
                    continue;
                }

                Directory.CreateDirectory(folder);
            }
        }

        /// <summary>
        /// Checks the permission.
        /// </summary>
        /// <returns>The permission.</returns>
        private async Task<PermissionStatus> CheckPermission()
        {
            /*
                Source Code: https://github.com/jamesmontemagno/PermissionsPlugin
            */

            // TODO
            // https://blog.xamarin.com/requesting-runtime-permissions-in-android-marshmallow/
            // only apply to Android API23+ or iOS8
            // shoudl add OS version checking here

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status == PermissionStatus.Granted)
                {
                    return PermissionStatus.Granted;
                }

                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    // TODO
                    // put your custom message here to notify user about the app will require to access some hardware
                    var page = Xamarin.Forms.Application.Current.MainPage;
                    await page.DisplayAlert("Fitness App", "Fitness App requires to access this device external storage?", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                return results[Permission.Storage];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(true, ex.Message);
            }

            return PermissionStatus.Denied;
        }

        /// <summary>
        /// Vibrate the phone for specified amount of time
        /// </summary>
        /// <param name="vibrateSpan">Time span to vibrate. 500ms is default if null</param>
        public void Vibration(TimeSpan? vibrateSpan = null)
        {
            using (var vibrator = (Android.OS.Vibrator)Android.App.Application.Context.GetSystemService(Context.VibratorService))
            {
                if ((int)Android.OS.Build.VERSION.SdkInt >= 11)
                {
#if __ANDROID_11__
                    if (!vibrator.HasVibrator)
                    {
                        Console.WriteLine("Android device does not have vibrator.");
                        return;
                    }
#endif
                }

                var milliseconds = vibrateSpan.HasValue ? vibrateSpan.Value.TotalMilliseconds : 500;
                if (milliseconds < 0)
                {
                    milliseconds = 0;
                }

                try
                {
                    vibrator.Vibrate(VibrationEffect.CreateOneShot((long)milliseconds, VibrationEffect.DefaultAmplitude));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to vibrate Android device, ensure VIBRATE permission is set.\r\n{ex.Message}");
                }
            }
        }
        public bool IsKitKatWithStepCounter(Object pm)
        {
            int currentApiVersion = (int)Build.VERSION.SdkInt;
            return currentApiVersion >= 19
                && ((Android.Content.PM.PackageManager)pm).HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter)
                && ((Android.Content.PM.PackageManager)pm).HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);

        }
        public bool AndroidStepSupport
        {
            get
            {
                var pm = Application.Context.PackageManager;
                int currentApiVersion = (int)Build.VERSION.SdkInt;
                return currentApiVersion >= 19
                    && pm.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter)
                    && pm.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);

            }
        }

    }
}
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using FitnessApp.Common;
using FitnessApp.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace FitnessApp.Droid.Receivers
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            int alarmId = intent.GetIntExtra("alarmId", 512);
            int notificationId = intent.GetIntExtra("notId", 1024);
            string message = intent.GetStringExtra("message");
            string title = intent.GetStringExtra("title");
            bool vibrate = intent.GetBooleanExtra("vibrate", false);

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, alarmId, notIntent, PendingIntentFlags.UpdateCurrent);
            var manager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            var builder = new NotificationCompat.Builder(context)
                            .SetContentIntent(contentIntent)
                            .SetContentTitle(title)
                            .SetSmallIcon(Resource.Drawable.icon_sil)
                            .SetDefaults((int)NotificationDefaults.Sound)
                            .SetContentText(message)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                            .SetAutoCancel(true);

            var notification = builder.Build();
            manager.Notify(notificationId, notification);

            if (vibrate)
            {
                var deviceUtility = ServiceLocator.Current.GetInstance<IDeviceUtility>();
                deviceUtility.Vibration();
            }
        }
    }
}
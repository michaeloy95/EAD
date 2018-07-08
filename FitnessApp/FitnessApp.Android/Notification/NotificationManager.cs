using Android.App;
using Android.Content;
using Android.Support.V4.App;
using FitnessApp.Common;
using FitnessApp.Droid.Receivers;
using FitnessApp.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System;

namespace FitnessApp.Droid.Notification
{
    public class NotificationManager : INotificationManager
    {
        private const int DefaultAlarmId = 512;
        private const int DefaultNotificationId = 1024;

        public NotificationManager()
        {
        }

        private Activity Context
        {
            get
            {
                var context = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;
                return context ?? (Activity)Application.Context;
            }
        }

        public void ShowNotification(string notificationName, string title, string message)
        {
            this.ShowNotification(notificationName, title, message, false);
        }

        public void ShowNotification(string notificationName, string title, string message, bool vibrate)
        {
            Android.App.NotificationManager manager = Application.Context.GetSystemService(Android.Content.Context.NotificationService) as Android.App.NotificationManager;
            if (manager == null)
            {
                return;
            }

            var builder = new NotificationCompat.Builder(this.Context)
                .SetContentTitle(title)
                .SetSmallIcon(GetNotificationIcon())
                .SetStyle(new NotificationCompat.BigTextStyle().BigText(message))
                .SetDefaults((int)NotificationDefaults.Sound)
                .SetContentText(message);

            manager.Notify(DefaultNotificationId, builder.Build());

            if (vibrate)
            {
                var deviceUtility = ServiceLocator.Current.GetInstance<IDeviceUtility>();
                deviceUtility.Vibration();
            }
        }
        
        public void ShowNotificationAlarm(string notificationName, string title, string message, int hour, bool repeating = false)
        {
            this.ShowNotificationAlarm(notificationName, title, message, false, hour, 0, repeating);
        }

        public void ShowNotificationAlarm(string notificationName, string title, string message, bool vibrate, int hour, bool repeating = false)
        {
            this.ShowNotificationAlarm(notificationName, title, message, vibrate, hour, 0, repeating);
        }

        public void ShowNotificationAlarm(string notificationName, string title, string message, int hour, int minute, bool repeating = false)
        {
            this.ShowNotificationAlarm(notificationName, title, message, false, hour, minute, repeating);
        }

        public void ShowNotificationAlarm(string notificationName, string title, string message, bool vibrate, int hour, int minute, bool repeating = false)
        {
            Intent alarmIntent = new Intent(this.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("alarmId", GetAlarmId(notificationName));
            alarmIntent.PutExtra("notId", GetNotificationId(notificationName));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            alarmIntent.PutExtra("vibrate", vibrate);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this.Context, GetAlarmId(notificationName), alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)this.Context.GetSystemService(Android.Content.Context.AlarmService);

            long nextAlarmMil = MillisToNextTime(hour, minute);
            if (repeating)
            {
                alarmManager.SetRepeating(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + nextAlarmMil, AlarmManager.IntervalDay, pendingIntent);
            }
            else
            {
                alarmManager.Set(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + nextAlarmMil, pendingIntent);
            }
        }

        public void ShowNotificationAlarmMorning()
        {
            this.ShowNotificationAlarm(Constants.NotificationAlarmMorning, "Let's Workout", "Feed your goals. Starve your doubt. Go workout today.", true, 9, 00, true);
        }

        public void ShowNotificationAlarmNoon()
        {
            this.ShowNotificationAlarm(Constants.NotificationAlarmNoon, "Don't Miss Your Workout", "Remember to tick your daily workout.", true, 15, 00, true);
        }

        public void ShowNotificationAlarmMeal()
        {
            this.ShowNotificationAlarm(Constants.NotificationOthers, "How's your diet today?", "Remember to record your meal to keep track on your calories.", true, 19, 00);
        }

        private int GetAlarmId(string notificationName)
        {
            if (notificationName == Constants.NotificationAlarmMorning)
            {
                return DefaultAlarmId + 1;
            }

            if (notificationName == Constants.NotificationAlarmNoon)
            {
                return DefaultAlarmId + 2;
            }

            if (notificationName == Constants.NotificationAlarmEvening)
            {
                return DefaultAlarmId + 3;
            }

            if (notificationName == Constants.NotificationOthers)
            {
                return DefaultAlarmId + 4;
            }

            return DefaultAlarmId;
        }

        private int GetNotificationId(string notificationName)
        {
            // Alarm Notification
            if (notificationName == Constants.NotificationAlarmMorning ||
                notificationName == Constants.NotificationAlarmNoon ||
                notificationName == Constants.NotificationAlarmEvening)
            {
                return DefaultNotificationId + 1;
            }

            return DefaultNotificationId;
        }

        private int GetNotificationIcon()
        {
            bool useWhiteIcon = (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop);
            return useWhiteIcon ? Resource.Drawable.icon_sil : Resource.Drawable.icon;
        }

        private long MillisToNextTime(int hour = 0, int minute = 0, int second = 0)
        {
            DateTime nextDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0, 0);
            nextDateTime = (nextDateTime > DateTime.Now) ? nextDateTime : nextDateTime.AddDays(1);

            return (long)(nextDateTime.Subtract(DateTime.Now)).TotalMilliseconds;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Interfaces
{
    public interface INotificationManager
    {
        void ShowNotification(string notificationName, string title, string message);

        void ShowNotification(string notificationName, string title, string message, bool vibrate);

        void ShowNotificationAlarm(string notificationName, string title, string message, int hour);

        void ShowNotificationAlarm(string notificationName, string title, string message, bool vibrate, int hour);

        void ShowNotificationAlarm(string notificationName, string title, string message, int hour, int minute);

        void ShowNotificationAlarm(string notificationName, string title, string message, bool vibrate, int hour, int minute);

        void ShowNotificationAlarmMorning();

        void ShowNotificationAlarmNoon();

        void ShowNotificationAlarmEvening();
    }
}

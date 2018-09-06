namespace FitnessApp.Interfaces
{
    public interface INotificationManager
    {
        void ShowNotification(string notificationName, string title, string message);

        void ShowNotification(string notificationName, string title, string message, bool vibrate);

        void ShowNotificationAlarm(string notificationName, string title, string message, int hour, bool repeating);

        void ShowNotificationAlarm(string notificationName, string title, string message, bool vibrate, int hour, bool repeating);

        void ShowNotificationAlarm(string notificationName, string title, string message, int hour, int minute, bool repeating);

        void ShowNotificationAlarm(string notificationName, string title, string message, bool vibrate, int hour, int minute, bool repeating);

        void ShowNotificationAlarmMorning();

        void ShowNotificationAlarmNoon();

        void ShowNotificationAlarmMeal();
    }
}

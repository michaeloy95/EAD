using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Board
{
    public class BoardViewModel : BaseViewModel
    {
        private int slideshowPosition = 0;
        public int SlideshowPosition
        {
            get { return slideshowPosition; }
            set { SetProperty<int>(ref slideshowPosition, value); }
        }

        private bool isPedometerSupported;
        public bool IsPedometerSupported
        {
            get { return isPedometerSupported; }
            set { SetProperty<bool>(ref isPedometerSupported, value); }
        }

        private double stepProgress;
        public double StepProgress
        {
            get { return stepProgress; }
            set { SetProperty<double>(ref stepProgress, value); }
        }

        private string stepProgressText;
        public string StepProgressText
        {
            get { return stepProgressText; }
            set { SetProperty<string>(ref stepProgressText, value); }
        }

        private IList<string> slideshowItemsSource;
        public IList<string> SlideshowItemsSource
        {
            get { return slideshowItemsSource; }
            set { SetProperty<IList<string>>(ref slideshowItemsSource, value); }
        }

        private IList<string> notifications;
        public IList<string> Notifications
        {
            get { return notifications.Take(1).ToList(); }
            set { SetProperty<IList<string>>(ref notifications, value); }
        }

        public string DisplayNotification
        {
            get { return notifications.Last(); }
        }

        public BoardViewModel()
        {
            SlideshowItemsSource = new List<string>
            {
                "http://www.electricprism.com/aeron/slideshow/images/1.jpg",
                "http://wowslider.com/sliders/demo-54/data1/images/dhowboat.jpg",
                "https://www.w3schools.com/w3css/img_fjords_wide.jpg",
                "http://www.123-slideshow.com/upload/picscloud/bilder/1.jpg"
            };

            Notifications = new List<string>
            {
                "Notifications 1",
                "Notifications 2",
                "Notifications 3",
                "Notifications 4",
                "Notifications 5",
                "Notifications 6",
                "Notifications 7",
                "Notifications 8",
                "Notifications 9",
                "Notifications 10",
            };

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                SlideshowPosition = ++SlideshowPosition % SlideshowItemsSource.Count;
                return true;
            });

            this.NotificationManager.ShowNotificationAlarmMorning();
            this.NotificationManager.ShowNotificationAlarmNoon();
            this.NotificationManager.ShowNotificationAlarmMeal();

            Refresh();
        }

        public void Refresh()
        {
            var deviceUtil = DependencyService.Get<Interfaces.IDeviceUtility>();
            this.IsPedometerSupported = deviceUtil.AndroidStepSupport;
            var message = DependencyService.Get<Interfaces.IMessageHelper>();
            message.ShortAlert(IsPedometerSupported.ToString());
            if (deviceUtil.AndroidStepSupport)
            {
                var stepsToday = Helpers.Settings.StepsToday;
                this.StepProgress = stepsToday / 10000;
                this.StepProgressText = stepsToday.ToString() + "/10,000";
            }
        }
    }
}

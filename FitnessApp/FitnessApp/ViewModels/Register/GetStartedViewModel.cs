using FitnessApp.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Register
{
    public class GetStartedViewModel : BaseViewModel
    {
        public ICommand StartCommand { get; private set; }

        public GetStartedViewModel()
        {
            StartCommand = new Command(GotoMainPage);
        }

        private void GotoMainPage()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
        }
    }
}
